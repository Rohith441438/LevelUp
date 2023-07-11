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

        //public void Post(Employee employee)
        //{
        //    using(EmployeeDBContext employeeDBContext = new EmployeeDBContext())
        //    {
        //        employeeDBContext.Employees.Add(employee);
        //        employeeDBContext.SaveChanges();
        //    }
        //}

        ////Here the above method will return a void cause of this we will get No Content response status which is not a good practice, to Solve this issue follow the below way of Posting
        ///

        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using(EmployeeDBContext employeeDBContext = new EmployeeDBContext())
                {
                    employeeDBContext.Employees.Add(employee);
                    employeeDBContext.SaveChanges();

                    //Always return a responsemessage after successfully post to DB, with statusCode 201
                    var responseMessage = Request.CreateResponse(HttpStatusCode.Created, employee);
                    //Try to add location of the response in the response message as like below
                    responseMessage.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());

                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                var responseMessage = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                
                return responseMessage;
            }
        }

        //Http Put verbs
        //public void Put(int id, [FromBody] Employee employee)
        //{
        //    using(EmployeeDBContext dbContext = new EmployeeDBContext())
        //    {
        //        var entity = dbContext.Employees.FirstOrDefault(x => x.ID == id);

        //        entity.FirstName = employee.FirstName;
        //        entity.LastName = employee.LastName;
        //        entity.Salary = employee.Salary;
        //        entity.Gender = employee.Gender;

        //        dbContext.SaveChanges();
        //    }
        //}
        ////The above method will work but it response back with 204 status code which is not a good practice, see the below method which will return a HttpResponseMessage

        public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        {
            try
            {
                using(EmployeeDBContext employeeDBContext = new EmployeeDBContext())
                {
                    var entity = employeeDBContext.Employees.FirstOrDefault(x => x.ID==id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " Not Found");
                    }
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.Salary = employee.Salary;
                    entity.Gender = employee.Gender;

                    employeeDBContext.SaveChanges();
                    
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //Http Delete verbs
        //public void Delete(int id)
        //{
        //    using (EmployeeDBContext dbContext = new EmployeeDBContext())
        //    {
        //        dbContext.Employees.Remove(dbContext.Employees.FirstOrDefault(x => x.ID == id));

        //        dbContext.SaveChanges();
        //    }
        //}
        ////The above method will work but it response back with 204 status code which is not a good practice, see the below method which will return a HttpResponseMessage

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBContext employeeDBContext = new EmployeeDBContext())
                {
                    var entity = employeeDBContext.Employees.FirstOrDefault(x => x.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " Not Found");
                    }
                    employeeDBContext.Employees.Remove(entity);

                    employeeDBContext.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
