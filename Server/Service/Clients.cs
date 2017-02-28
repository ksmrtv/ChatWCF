using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Data;

namespace Service
{
    public static class Clients
    {
        public static List<ClientInfo> listClients = new List<ClientInfo>();

        public static void SendUsersToClients()
        {
            using (DataModelContainer db = new DataModelContainer())
            {
                int[] usersOnlineId = listClients.Select(x => x.UserId).ToArray();
                string[] userList = db.Users.Where(x => usersOnlineId.Contains(x.Id)).Select(x => x.Login).ToArray();

                for (int j = 0; j < listClients.Count; j++)
                    listClients[j].callback.ReceiveUsers(userList);
            }
        }

        public static void SendPublicMessageToClients(object data)
        {
            Message mess = (Message)data;
            for (int i = 0; i < listClients.Count; i++)
                listClients[i].callback.ReceivePublicMessage(GetLoginFrom(mess), mess.Text, mess.Date);
        }

        public static void SendLastPublicMessageToClient(object data)
        {
            int userId = (int)data;
            ClientInfo clientTo = listClients.FirstOrDefault(x => x.UserId == userId);
            using (DataModelContainer db = new DataModelContainer())
            {
                var lastMess = db.Messages.Where(x => x.UserTo_Id == null && x.GroupTo_Id == null).OrderBy(x => x.Date).Take(5);
                foreach (var mess in lastMess)
                    clientTo.callback.ReceivePublicMessage(GetLoginFrom(mess), mess.Text, mess.Date);
            }
        }

        public static void SendPrivateMessageToClients(object data)
        {
            Message mess = (Message)data;
            if(mess.UserTo_Id != null) // шлем личное сообщение пользователю
            {
                ClientInfo clientTo = listClients.FirstOrDefault(x => x.UserId == mess.UserTo_Id);
                if(clientTo != null)
                {
                    bool res = clientTo.callback.ReceivePrivateMessage(GetLoginFrom(mess), GetLoginTo(mess), mess.Text, mess.Date);
                    if(res)
                        using (var db = new DataModelContainer())
                        {
                            Message message = db.Messages.FirstOrDefault(x => x.Id == mess.Id);
                            message.IsRead = true;
                            db.Entry(message).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                }
                ClientInfo clientFrom = listClients.FirstOrDefault(x => x.UserId == mess.UserFrom_Id);
                if (clientFrom != null && clientFrom != clientTo)
                    clientFrom.callback.ReceivePrivateMessage(GetLoginFrom(mess), GetLoginTo(mess), mess.Text, mess.Date);
            }
            else if (mess.GroupTo_Id != null) //шлем групповое сообщение
            {
                using (var db = new DataModelContainer())
                {
                    Group grp = db.Groups.FirstOrDefault(x => x.Id == mess.GroupTo_Id);
                    foreach (var us in grp.User)
                    {
                        ClientInfo client = listClients.FirstOrDefault(x => x.UserId == us.Id);
                        if (client != null)
                        {
                            bool res = client.callback.ReceivePrivateMessage(GetLoginFrom(mess), GetGroupTo(mess), mess.Text, mess.Date);
                            if (res)
                            {
                                ReadMessage rm = db.ReadMessages.FirstOrDefault(x => x.Message_Id == mess.Id && x.User_Id == client.UserId);
                                rm.IsRead = true;
                                db.Entry(rm).State = System.Data.Entity.EntityState.Modified;
                            }
                        }
                    }
                    db.SaveChanges();
                }
            }
        }

        public static void SendUnreadPrivateMessageToClient(object data)
        {
            int userId = (int)data;
            ClientInfo clientTo = listClients.FirstOrDefault(x => x.UserId == userId);
            using (DataModelContainer db = new DataModelContainer())
            {
                int[] messIds = db.ReadMessages.Where(x => x.User_Id == userId && x.IsRead == false).Select(x => x.Message_Id).ToArray();
                var unreadMess = db.Messages.Where(x => (x.UserTo_Id == userId && x.IsRead == false) || messIds.Contains(x.Id)).OrderBy(x => x.Date); // все непрочитанные сообщения, личные и групповые

                foreach (var mess in unreadMess)
                {
                    if(mess.UserTo_Id != null) //личное сообщение
                    {
                        bool res = clientTo.callback.ReceivePrivateMessage(GetLoginFrom(mess), GetLoginTo(mess), mess.Text, mess.Date);
                        if (res)
                            { 
                                mess.IsRead = true;
                                db.Entry(mess).Property(c => c.IsRead).IsModified = true;
                            }
                    }
                    else //групповое сообщение
                    {
                        bool res = clientTo.callback.ReceivePrivateMessage(GetLoginFrom(mess), GetGroupTo(mess), mess.Text, mess.Date);
                        if (res)
                        {
                            ReadMessage rm = db.ReadMessages.FirstOrDefault(x => x.Message_Id == mess.Id && x.User_Id == userId);
                            rm.IsRead = true;
                            db.Entry(rm).Property(c => c.IsRead).IsModified = true;
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public static void SendGroupsToClient(object data)
        {
            int id = (int)data;
            using (var db = new DataModelContainer())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == id);
                ClientInfo client = listClients.FirstOrDefault(x => x.UserId == id);
                if (client == null)
                    return;
                string[] grps = user.Groups.Select(x => x.Name).ToArray();
                client.callback.ReceiveGroups(grps);
            }
        }

        private static string GetLoginFrom(Message mess)
        {
            using (DataModelContainer db = new DataModelContainer())
            {
                return db.Users.FirstOrDefault(x => x.Id == mess.UserFrom_Id).Login;
            }
        }

        private static string GetLoginTo(Message mess)
        {
            using (DataModelContainer db = new DataModelContainer())
            {
                return db.Users.FirstOrDefault(x => x.Id == mess.UserTo_Id).Login;
            }
        }

        private static string GetGroupTo(Message mess)
        {
            using (DataModelContainer db = new DataModelContainer())
            {
                return db.Groups.FirstOrDefault(x => x.Id == mess.GroupTo_Id).Name;
            }
        }
    }
}