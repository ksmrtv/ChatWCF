using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace Service
{
    public class ClientInfo
    {
        public IServiceSendMessageCallBack callback;

        int userId;
        public int UserId {  get { return userId; } }

        //List<Message> MessageList;

        public ClientInfo()
        { }

        public ClientInfo(int id)
        {
            callback = null;
            userId = id;
            //MessageList = new List<Message>();
        }
    }
}