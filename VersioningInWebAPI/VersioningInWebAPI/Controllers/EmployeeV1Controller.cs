using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VersioningInWebAPI.Models;

namespace VersioningInWebAPI.Controllers
{
    public class EmployeeV1Controller : ApiController
    {
        public IEnumerable<EmployeeV1> employees = new List<EmployeeV1>()
        {
            new EmployeeV1{ EmployeeId = 101, EmployeeName = "Rohith"},
            new EmployeeV1{ EmployeeId = 102, EmployeeName = "Vinay"},
            new EmployeeV1{ EmployeeId = 103, EmployeeName = "Praharshini"},
            new EmployeeV1{ EmployeeId = 104, EmployeeName = "Pravihith"}            
        };

        
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, employees.ToList());
        }
    }
}
