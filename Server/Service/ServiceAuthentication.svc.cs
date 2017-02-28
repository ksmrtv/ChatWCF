using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Data;
using System.Data.Entity;
using System.Security.Cryptography;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceAuthentication : IServiceAuthentication
    {
        public UserValidation RegisterUser(string login, string pass)
        {
            using (DataModelContainer db = new Data.DataModelContainer())
            {
                foreach (var u in db.Users)
                {
                    if (u.Login.Equals(login))
                        return UserValidation.LoginAlreadyExists;
                }

                string passdSalt = GenerateSalt();
                User user = new User { Login = login, PasswordSalt = passdSalt, Password = Hash(pass, passdSalt) };
                db.Users.Add(user);
                db.SaveChanges();
                return UserValidation.RegistrationSuccess;
            }
        }

        public UserValidation Enter(string login, string password)
        {
            using (DataModelContainer db = new DataModelContainer())
            {
                var user = db.Users.FirstOrDefault(x => x.Login.Equals(login));

                if (user != null)
                {
                    if (ValidatePassworg(password, user.Password, user.PasswordSalt))
                        return UserValidation.EnterSuccess;
                    else
                        return UserValidation.IncorrectPassword;
                }
                return UserValidation.LoginNotFound;
            }
        }

        private static string GenerateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[8];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        private static string Hash(string value, string salt)
        {
            string saltedvalue = value + salt;
            byte[] saltedvb = Encoding.UTF8.GetBytes(saltedvalue);
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(saltedvb));
        }

        public bool ValidatePassworg(string pass, string passFromDb, string SaltFromDb)
        {
            string passwordHash = Hash(pass, SaltFromDb);
            return passFromDb.SequenceEqual(passwordHash);
        }
    }
}
