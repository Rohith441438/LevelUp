using EmployeeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (EmployeeDBContext dbContext = new EmployeeDBContext())
            {
                var employees = dbContext.Employees.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
        }

        public HttpResponseMessage GetById(int id)
        {
            using(EmployeeDBContext dbContext = new EmployeeDBContext())
            {
                var employee = dbContext.Employees.FirstOrDefault(x => x.ID == id);
                
                if(employee != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id "+id.ToString() + " is not found");
                }
            }
        }
    }
}
