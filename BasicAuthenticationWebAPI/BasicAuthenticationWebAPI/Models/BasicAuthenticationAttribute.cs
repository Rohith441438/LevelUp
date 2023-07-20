using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BasicAuthenticationWebAPI.Models
{
    public class BasicAuthenticationAttribute:AuthorizationFilterAttribute
    {
        private const string Realm = "My Realm";
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //If Authorization header is Null
            //Then return unAuthorized
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

                //If request was unauthorized, then add WWW-Authenticate header to the response which indicates that it require basic Authentication
                if(actionContext.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
                }
            }
            else
            {
                try
                {
                    //Get Authentication Token from Request Headers
                    string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

                    //decode the string
                    string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                    //convert the string into a string array
                    string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');

                    string username = usernamePasswordArray[0];
                    string password = usernamePasswordArray[1];

                    if (UserValidate.Login(username, password))
                    {
                        var identity = new GenericIdentity(username);

                        IPrincipal principal = new GenericPrincipal(identity, null);
                        Thread.CurrentPrincipal = principal;

                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.User = principal;
                        }
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    }
                }
                catch (Exception ex)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
    }
}