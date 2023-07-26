using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using VersioningInWebAPI.Models;

namespace VersioningInWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //The below Routes are for Versioning using URL
            //config.Routes.MapHttpRoute(
            //    name: "Version1",
            //    routeTemplate: "api/v1/employee/{id}",
            //    defaults: new { id = RouteParameter.Optional, controller = "EmployeeV1" }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "Version2",
            //    routeTemplate: "api/v2/employee/{id}",
            //    defaults: new { id = RouteParameter.Optional, controller = "EmployeeV2" }
            //);

            //Replacing CustomControllerSelector for IHttpControllerSelector
            config.Services.Replace(typeof(IHttpControllerSelector), new CustomControllerSelector(config));

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{id}",
                defaults : new {id = RouteParameter.Optional }
            );
        }
    }
}
