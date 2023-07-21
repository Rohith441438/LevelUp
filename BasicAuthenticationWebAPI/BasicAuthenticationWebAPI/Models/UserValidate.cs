using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuthenticationWebAPI.Models
{
    public class UserValidate
    {
        //This method is used to validate the user credentials
        public static bool Login(string username, string password)
        {
            UserBL userBL = new UserBL();
            var users = userBL.GetUsers();
            return users.Any(x => x.UserName.ToLower() == username.ToLower() && x.Password.ToLower() == password.ToLower());
        }

        public static User GetUserDetails(string username, string password)
        {
            var userBL = new UserBL();
            return userBL.GetUsers().FirstOrDefault(x => x.UserName == username && x.Password == password);
        }
    }
}