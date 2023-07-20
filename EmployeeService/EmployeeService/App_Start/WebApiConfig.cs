using EmployeeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;

namespace EmployeeService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //After creating Custom Route Constraint, we need to add this to config
            //follow the below steps
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("nonZero", typeof(NonZeroConstraint));

            config.MapHttpAttributeRoutes(constraintResolver);
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Here in the below, How Routing and Action Selection can be done in ASP.net
            //Convention-based Routing
            config.Routes.MapHttpRoute(
                name:"ActionAPI",
                routeTemplate:"api/Employee/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"\d+" }
            );
            //In the above example we Added one new Schema, any URL that matches the above schema will be directed to the respective Action method in Employee controller

            //CORS - Cross Origin Resource Sharing
            //If we are accessing an API which is from different origin then browsers will not allow as they are not from same origin, in this case we should Enablecors in that API, lets see how we can Enable

            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            //Here the three parameters are Origins, Headers and Methods, we provide * means we are allowing all origins, headers and methods
            //we can provide comma separated Origins, Headers and methods in the parameters
            config.EnableCors(cors);
        }
    }
}
