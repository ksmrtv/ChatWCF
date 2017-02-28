using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Service
{
    [ServiceContract]
    public interface IServiceAuthentication
    {
        [OperationContract]
        UserValidation RegisterUser(string login, string pass);

        [OperationContract]
        UserValidation Enter(string login, string password);
    }

    public enum UserValidation {RegistrationSuccess, EnterSuccess, IncorrectPassword, LoginNotFound, LoginAlreadyExists}
}
