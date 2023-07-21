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
                UserName = "AdminUser",
                Password = "AdGHkl",
                Roles = "Admin",
                Email = "AdminUser@gmail.com"
            });
            users.Add(new User()
            {
                ID = 121,
                UserName = "BothUser",
                Password = "OksKfnsD",
                Roles = "Admin, SuperAdmin",
                Email = "BothUser@gmail.com"
            });
            users.Add(new User()
            {
                ID = 131,
                UserName = "SuperAdminUser",
                Password = "JHsofgnso",
                Roles = "SuperAdmin",
                Email = "SuperAdminUser@gmail.com"
            });

            return users;
        }
    }
}