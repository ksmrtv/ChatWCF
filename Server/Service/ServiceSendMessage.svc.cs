using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Data;
using System.Threading;

namespace Service
{
    public class ServiceSendMessage : IServiceSendMessage
    {
        public int Subscribe(string login)
        {
            int id = 0;
            using (DataModelContainer db = new DataModelContainer())
            {
                id = db.Users.First(x => x.Login.Equals(login)).Id;
            }
            ClientInfo client = Clients.listClients.FirstOrDefault(x => x.UserId == id);
            if (client != null)
                Unsubscribe(id);
            client = new ClientInfo(id);
            client.callback = OperationContext.Current.GetCallbackChannel<IServiceSendMessageCallBack>();
            Clients.listClients.Add(client);

            Thread thread = new Thread(new ThreadStart(Clients.SendUsersToClients));
            thread.IsBackground = true;
            thread.Start();

            Thread thread1 = new Thread(new ParameterizedThreadStart(Clients.SendLastPublicMessageToClient));
            thread1.IsBackground = true;
            thread1.Start(id);

            Thread thread2 = new Thread(new ParameterizedThreadStart(Clients.SendUnreadPrivateMessageToClient));
            thread2.IsBackground = true;
            thread2.Start(id);

            Thread thread3 = new Thread(new ParameterizedThreadStart(Clients.SendGroupsToClient));
            thread3.IsBackground = true;
            thread3.Start(id);

            return id;
        }

        public void Unsubscribe(int id)
        {
            ClientInfo client = Clients.listClients.FirstOrDefault(x => x.UserId == id);
            if (client == null)
                return;
            Clients.listClients.Remove(client);

            Thread thread = new Thread(new ThreadStart(Clients.SendUsersToClients));
            thread.IsBackground = true;
            thread.Start();
        }

        public void SendPublicMessage(int idUserFrom, string message)
        {
            using (DataModelContainer db = new DataModelContainer())
            {
                Message mess = new Message { UserFrom_Id = idUserFrom, UserTo_Id = null, GroupTo_Id = null, Text = message, Date = DateTime.Now, IsRead = null };
                db.Messages.Add(mess);
                db.SaveChanges();

                Thread thread = new Thread(new ParameterizedThreadStart(Clients.SendPublicMessageToClients));
                thread.IsBackground = true;
                thread.Start(mess);
            }
        }

        public bool SendPrivateMessage(int idUserFrom, string idTo, string text)
        {
            using (DataModelContainer db = new DataModelContainer())
            {
                var userFrm = db.Users.FirstOrDefault(x => x.Id == idUserFrom);
                var userT = db.Users.FirstOrDefault(x => x.Login.Equals(idTo));
                if (userT != null) //личное сообщение
                {
                    Message mess = new Message { UserFrom_Id = idUserFrom, UserTo_Id = userT.Id, GroupTo_Id = null, Text = text, Date = DateTime.Now, IsRead = false };
                    ReadMessage readMess = new ReadMessage { Message_Id = mess.Id, User_Id = null, IsRead = false };
                    db.Messages.Add(mess);
                    db.ReadMessages.Add(readMess);
                    db.SaveChanges();

                    Thread thread = new Thread(new ParameterizedThreadStart(Clients.SendPrivateMessageToClients));
                    thread.IsBackground = true;
                    thread.Start(mess);
                    return true;
                }
                else //групповое сообщение
                {
                    var grp = db.Groups.FirstOrDefault(x => x.Name.Equals(idTo));
                    if (grp == null)
                         return false;
                    else
                    {
                        Message mess = new Message { UserFrom_Id = idUserFrom, UserTo_Id = null, GroupTo_Id = grp.Id, Text = text, Date = DateTime.Now, IsRead = null };
                        db.Messages.Add(mess);

                        foreach (var us in grp.User)
                        {
                              ReadMessage readMess = new ReadMessage { Message_Id = mess.Id, User_Id = us.Id, IsRead = false };
                              db.ReadMessages.Add(readMess);
                        }
                        db.SaveChanges();

                        Thread thread = new Thread(new ParameterizedThreadStart(Clients.SendPrivateMessageToClients));
                        thread.IsBackground = true;
                        thread.Start(mess);
                        return true;
                    }
                }
            }
        }

        public string[] GetAllUsers()
        {
            using (var db = new DataModelContainer())
            {
                var users = db.Users.OrderBy(x => x.Login);
                string[] usrs = new string[users.Count()];
                int i = 0;
                foreach (var user in users)
                    usrs[i++] = user.Login;
                return usrs;
            }
        }

        public GroupValidation CreateGroup(string name, string[] users)
        {
            using (var db = new DataModelContainer())
            {
                if (db.Groups.FirstOrDefault(x => x.Name.Equals(name)) != null)
                    return GroupValidation.GroupNameIsExists;

                Group grp = new Group { Name = name };
                int[] ids = new int[users.Count()];
                for (int i = 0; i < users.Count(); i++)
                {
                    string login = users[i];
                    User user = db.Users.FirstOrDefault(x => x.Login.Equals(login));
                    ids[i] = user.Id;
                    grp.User.Add(user);
                }
                db.Groups.Add(grp);
                db.SaveChanges();

                for (int j = 0; j < ids.Length; j++)
                {
                    int id = ids[j];
                    if(Clients.listClients.FirstOrDefault(x => x.UserId == id) != null)
                    {
                        Thread thread = new Thread(new ParameterizedThreadStart(Clients.SendGroupsToClient));
                        thread.IsBackground = true;
                        thread.Start(id);
                    }
                }
                return GroupValidation.GroupCreateSuccess;
            }
        }

        public GroupValidation EditGroup(string oldname, string newname, string[] users)
        {
            using (var db = new DataModelContainer())
            {
                Group grp = db.Groups.FirstOrDefault(x => x.Name.Equals(oldname));
                if (grp == null)
                    return GroupValidation.GroupNotFound;

                if(!grp.Name.Equals(newname))
                    grp.Name = newname;

                var existingUsersinGroup = grp.User;
                List<User> removedUsers = new List<User>();
                foreach (var us in existingUsersinGroup) // находим пользователей, которых удалили
                    if (!users.Contains(us.Login))
                        removedUsers.Add(us);
                foreach (var ru in removedUsers)
                    grp.User.Remove(ru);

                for(int i=0; i < users.Length; i++) //находим пользователей, которых добавили
                {
                    string login = users[i];
                    if(existingUsersinGroup.FirstOrDefault(x => x.Login.Equals(login)) == null)
                    {
                        var userAdd = db.Users.FirstOrDefault(x => x.Login.Equals(login));
                        grp.User.Add(userAdd);
                    }
                }
                db.SaveChanges();


                for (int k = 0; k < users.Length; k++)
                {
                    string login = users[k];
                    int id = grp.User.FirstOrDefault(x => x.Login.Equals(login)).Id;
                    if (Clients.listClients.FirstOrDefault(x => x.UserId == id) != null)
                    {
                        Thread thread = new Thread(new ParameterizedThreadStart(Clients.SendGroupsToClient));
                        thread.IsBackground = true;
                        thread.Start(id);
                    }
                }
                for (int k = 0; k < removedUsers.Count; k++)
                {
                    string login = removedUsers[k].Login;
                    int id = db.Users.FirstOrDefault(x => x.Login.Equals(login)).Id;
                    if (Clients.listClients.FirstOrDefault(x => x.UserId == id) != null)
                    {
                        Thread thread = new Thread(new ParameterizedThreadStart(Clients.SendGroupsToClient));
                        thread.IsBackground = true;
                        thread.Start(id);
                    }
                }
                return GroupValidation.GroupEditSuccess;
            }
        }

        public string[] GetUsersFromGroup(string grpName)
        {
            using (var db = new DataModelContainer())
            {
                var group = db.Groups.FirstOrDefault(x => x.Name.Equals(grpName));
                var users = group.User;
                string[] usrs = new string[users.Count()];
                int i = 0;
                foreach (var us in users)
                    usrs[i++] = us.Login;
                return usrs;
            }
        }
    }
}
