using BasicAuthenticationWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace BasicAuthenticationWebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        [BasicAuthentication]
        [MyAthorize(Roles ="Admin")]
        [Route("api/Employee/AllMaleEmployees")]
        public HttpResponseMessage GetAllMaleEmployees()
        {
            //string username = Thread.CurrentPrincipal.Identity.Name;

            var employeesList = new EmployeeBL().GetEmployees();

            var identity = (ClaimsIdentity)User.Identity;

            //Username
            var userName = identity.Name;
            
            //Email
            var Email = identity.Claims.FirstOrDefault(x => x.Type == "Email").Value;

            //ID
            var Id = identity.Claims.FirstOrDefault(x => x.Type == "ID").Value;

            //as like above we can retreive the Authenticated user details

            return Request.CreateResponse(HttpStatusCode.OK, employeesList.Where(x => x.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)).ToList());
        }

        [BasicAuthentication]
        [MyAthorize(Roles ="SuperAdmin")]
        [Route("api/Employee/AllFemaleEmployees")]
        public HttpResponseMessage GetAllFemaleEmployees()
        {
            var femaleEmployeesList = new EmployeeBL().GetEmployees().Where(x => x.Gender == "Female").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, femaleEmployeesList);
        }

        [BasicAuthentication]
        [MyAthorize(Roles ="Admin, SuperAdmin")]
        [Route("api/Employee/AllEmployees")]
        public HttpResponseMessage GetAllEmployees()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new EmployeeBL().GetEmployees());
        }

        [BasicAuthentication]
        [MyAthorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Employee/GetEmployees")]
        public HttpResponseMessage GetEmployees()
        {
            var allEmployees = new EmployeeBL().GetEmployees();
            var idendtity = (ClaimsIdentity)User.Identity;
            var userName = idendtity.Name;
            switch (userName)
            {
                case "AdminUser":
                    return Request.CreateResponse(HttpStatusCode.OK, allEmployees.Where(x => x.Gender == "Male"));
                case "SuperAdminUser":
                    return Request.CreateResponse(HttpStatusCode.OK, allEmployees.Where(x => x.Gender == "Female"));
                default:
                    return Request.CreateResponse(HttpStatusCode.OK, allEmployees);
            }            
        }
    }
}
