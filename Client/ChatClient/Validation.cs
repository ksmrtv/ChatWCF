using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public static class Validation
    {
        public static Dictionary<ServiceAuthenticationReference.UserValidation, string> dictionaryUserValidation = 
            new Dictionary<ServiceAuthenticationReference.UserValidation, string>();
        public static Dictionary<ServiceMessageReference.GroupValidation, string> dictionaryGroupValidation =
            new Dictionary<ServiceMessageReference.GroupValidation, string>();

        static Validation()
        {
            dictionaryUserValidation.Add(ServiceAuthenticationReference.UserValidation.EnterSuccess, "");
            dictionaryUserValidation.Add(ServiceAuthenticationReference.UserValidation.IncorrectPassword, "Неверный пароль");
            dictionaryUserValidation.Add(ServiceAuthenticationReference.UserValidation.LoginAlreadyExists, "Такой логин уже существует");
            dictionaryUserValidation.Add(ServiceAuthenticationReference.UserValidation.LoginNotFound, "Логин не найден");
            dictionaryUserValidation.Add(ServiceAuthenticationReference.UserValidation.RegistrationSuccess, "Вы зарегистрированы. Нажмите Войти для входа");
            dictionaryGroupValidation.Add(ServiceMessageReference.GroupValidation.GroupNameIsExists, "Группа с таким именем уже сущесвует");

        }

        public static string FillAllFields { get { return "Заполните все поля"; } }
    }
}
