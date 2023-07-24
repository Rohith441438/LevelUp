using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace TokenBasedAuthenticationInWebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        //Token-based Authentication in Web API
        //Here in token-based authentication first user will provide his credentials and get a token, this token will contain all the necessary information about user with expiration date as well, after provided the token user can access resources what he want
        //In order to implement Token based authentication we need to install few packages into our project, those are :
        //Microsoft.Owin.Host.SystemWeb
        //Microsoft.Owin.Security.OAuth
        //Microsoft.Owin.Cors
        //Newtonsoft.json

        //The First step after adding the above packages is 
        //Create a class which is responsible to validate the user by taking two parameters, i.e, username and password, here in this case our class name UserMasterRepository, please check that file
        //Second step is to Add a class to validate user credentials asking for tokens, this class should be inherited from OAuthAuthorizationServerProvider class, please check it once
        //here we need to override two methods ValidateClientAuthentication and GrantResourceOwnerCredentials
        //ValidateClientAuthentication will be responsible for validating the client application
        //GrantResourceOwnerCredentials will be responsible for validating client credentials, if they found the credentials are fine then only we will get the token
        //Next step is to add OwinStarup file in App-Start
        //here we will configure the OAuth Authorization Server, please have a look

        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public HttpResponseMessage Get()
        {
            var identity = (ClaimsIdentity)User.Identity;

            return Request.CreateResponse(HttpStatusCode.OK, identity.Name);
        }
    }
}
