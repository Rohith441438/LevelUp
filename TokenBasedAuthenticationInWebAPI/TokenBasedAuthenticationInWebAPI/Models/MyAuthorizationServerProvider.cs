using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace TokenBasedAuthenticationInWebAPI.Models
{
    public class MyAuthorizationServerProvider :OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            if(!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.SetError("Invalid_Client", "Client credentials chouldnot be retrieved through the Authorization Header");
                context.Rejected();
                return;
            }
            //ClientMaster class is prototype of Client Data
            ClientMaster client = new ClientMasteryRepository().ValidateUser(clientId, clientSecret);
            if(client != null) 
            {
                context.OwinContext.Set<ClientMaster>("oauth:client", client);
                context.Validated(clientId);
            }
            else
            {
                context.SetError("Invalid_Client", "Client credentials are invalid");
                context.Rejected();
            }
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using(UserMasteryRepository _repo = new UserMasteryRepository())
            {
                var user = _repo.ValidateUser(context.UserName, context.Password);
                if(user == null)
                {
                    context.SetError("Invalid Grant", "Provided username and password are incorrect");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                var client = context.OwinContext.Get<ClientMaster>("oauth:client");
                identity.AddClaim(new Claim(ClaimTypes.Role, user.UserRoles));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim("Email", user.UserEmailID));
                identity.AddClaim(new Claim("ClientID", client.ClientId));
                identity.AddClaim(new Claim("ClientSecret", client.ClientSecret));
                identity.AddClaim(new Claim("ClientName", client.ClientName));

                context.Validated(identity);
            }
        }
    }
}