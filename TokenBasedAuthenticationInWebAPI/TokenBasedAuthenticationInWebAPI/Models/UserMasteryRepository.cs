using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenBasedAuthenticationInWebAPI.Models
{
    public class UserMasteryRepository : IDisposable
    {
        public SECURITY_DBEntities context = new SECURITY_DBEntities();
        public UserMaster ValidateUser(string username, string password)
        {
            var users = context.UserMasters.ToList();

            return users.FirstOrDefault(user => user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && user.UserPassword.Equals(password, StringComparison.OrdinalIgnoreCase));
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}