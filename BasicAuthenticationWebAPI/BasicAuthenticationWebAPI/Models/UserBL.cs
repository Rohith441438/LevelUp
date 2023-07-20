using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuthenticationWebAPI.Models
{
    public class UserBL
    {
        public List<User> GetUsers()
        {
            // In Real-time you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we hardcoded the data
            var users = new List<User>();
            users.Add(new User() 
            { 
                ID = 101,
                UserName = "MaleUser",
                Password = "AdGHkl",
            });
            users.Add(new User()
            {
                ID = 121,
                UserName = "FemaleUser",
                Password = "OFwlJdlsd",
            });

            return users;
        }
    }
}