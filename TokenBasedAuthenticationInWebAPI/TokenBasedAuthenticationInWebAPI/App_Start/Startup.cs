using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using TokenBasedAuthenticationInWebAPI.Models;

[assembly: OwinStartup(typeof(TokenBasedAuthenticationInWebAPI.App_Start.Startup))]

namespace TokenBasedAuthenticationInWebAPI.App_Start
{
    public class Startup
    {
        //This class will configure the OAuth Authorication server
        public void Configuration(IAppBuilder app)
        {
            //Enable CORS to allow the clients from different origins
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,

                //The path for generating the token
                TokenEndpointPath = new PathString("/token"),

                //Setting the token expiration
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(5),

                //Provide the class which is validating the user credentials
                Provider = new MyAuthorizationServerProvider()
            };

            //token generations
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
