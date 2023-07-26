using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenBasedAuthenticationInWebAPI.Models
{
    public class ClientMasteryRepository : IDisposable
    {
        public SECURITY_DBEntities context = new SECURITY_DBEntities();
        public ClientMaster ValidateUser(string ClientID, string ClientSecret)
        {
            var clients = context.ClientMaster.ToList();

            return clients.FirstOrDefault(user => user.ClientId.Equals(ClientID, StringComparison.OrdinalIgnoreCase) && user.ClientSecret.Equals(ClientSecret, StringComparison.OrdinalIgnoreCase));
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}