using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VersioningInWebAPI.Models;

namespace VersioningInWebAPI.Controllers
{
    public class EmployeeV2Controller : ApiController
    {
        public IEnumerable<EmployeeV2> employees = new List<EmployeeV2>()
        {
            new EmployeeV2{ EmployeeId = 101, EmployeeName = "Rohith", EmployeeAge = 23},
            new EmployeeV2{ EmployeeId = 102, EmployeeName = "Vinay", EmployeeAge = 23},
            new EmployeeV2{ EmployeeId = 103, EmployeeName = "Praharshini", EmployeeAge = 24},
            new EmployeeV2{ EmployeeId = 104, EmployeeName = "Pravihith", EmployeeAge = 23}
        };
        
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, employees.ToList());
        }
    }
}
