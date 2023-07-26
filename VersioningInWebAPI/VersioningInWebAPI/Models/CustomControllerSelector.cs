using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace VersioningInWebAPI.Models
{
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        //Here this class is responsible for selecing Controller based on the QueryString
        private HttpConfiguration config;
        public CustomControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            config = configuration;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //First fetch all the available controllers in API
            var controllers = GetControllerMapping();

            //get the controller name and parameter values from the requested URI
            var routeData = request.GetRouteData();

            //Get controller name from teh routeData
            var controllerName = routeData.Values["controller"].ToString();

            //get the version number from query string
            var versionNumber = "1";

            //var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);

            var customerHeader = "X-EmployeeService-Header";            
            if (request.Headers.Contains(customerHeader))
            {
                //retreiving the version number from headers
                versionNumber = request.Headers.GetValues(customerHeader).FirstOrDefault();
            }

            if(versionNumber == "1")
            {
                controllerName += "V1";
            }
            else
            {
                controllerName += "V2";
            }

            HttpControllerDescriptor controllerDescriptor;
            if(controllers.TryGetValue(controllerName, out controllerDescriptor))
            { return controllerDescriptor; }

            return null;
        }
    }
}