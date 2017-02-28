using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceSendMessage" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IServiceSendMessageCallBack))]
    public interface IServiceSendMessage
    {
        [OperationContract]
        int Subscribe(string login);

        [OperationContract(IsOneWay = true)]
        void Unsubscribe(int id);

        [OperationContract(IsOneWay =true)]
        void SendPublicMessage(int idUserFrom, string message);

        [OperationContract]
        bool SendPrivateMessage(int idUserFrom, string idTo, string text);

        [OperationContract]
        string[] GetAllUsers();

        [OperationContract]
        GroupValidation CreateGroup(string name, string[] users);

        [OperationContract]
        GroupValidation EditGroup(string oldname, string newName, string[] users);

        [OperationContract]
        string[] GetUsersFromGroup(string grpName);
    }

    public interface IServiceSendMessageCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveUsers(string[] users);

        [OperationContract(IsOneWay =true)]
        void ReceivePublicMessage(string from, string mess, DateTime date);

        [OperationContract]
        bool ReceivePrivateMessage(string from, string to, string mess, DateTime date);

        [OperationContract(IsOneWay = true)]
        void ReceiveGroups(string[] groups);
    }

    public enum GroupValidation { GroupNameIsExists, GroupCreateSuccess, GroupCreateError, GroupNotFound, GroupEditSuccess }
}
