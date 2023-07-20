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
            return users.Any(x => x.UserName == username && x.Password == password);
        }
    }
}