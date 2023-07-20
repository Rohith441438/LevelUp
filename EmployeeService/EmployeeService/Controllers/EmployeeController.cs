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

        //If method name is Get or starts with Get automatically it will be considered as a Get verb, if we want to have other than Get or Starts with Get but want that method to be Get method then we need to Add one attribute that is HttpGet
        //See the below example
        //Here as we already have a get method, asp.net is not going to consider these two methods as it considers them as one
        //two resolve this issue we should follow custom action names
        //
        [HttpGet]
        public HttpResponseMessage LoadAllMaleEmployees()
        {
            try
            {
                using (EmployeeDBContext dbContext = new EmployeeDBContext())
                {
                    var maleEmployees = dbContext.Employees.Where(x => x.Gender == "Male").ToList();
                    if (maleEmployees.Any())
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, maleEmployees);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Male employees found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
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

        //parameter binding
        //All the default data types are considered as the parameters from URI by default
        //If we pass any complex data types like custom data types are considered as the parameters form Body by default
        //but still if we want to force any parameter to be accessed from URI then need to apply [FromUri] Attribute to it and from Body then [FromBody] need to be applied
        //


        //RouteName
        //We are apply Route name for each Route, it will help us in generating links which we can add in the response url
        //Lets see the example
        [HttpGet]
        [Route("{stdId:nonZero}", Name = "GetEmployeeByIdRouteName")]
        public HttpResponseMessage GetEmployeeByIdRouteName(int strId)
        {
            using (EmployeeDBContext dBContext = new EmployeeDBContext())
            {
                return Request.CreateResponse(HttpStatusCode.OK, dBContext.Employees.FirstOrDefault(x => x.ID == strId));
            }            
        }

        [HttpPost]
        [Route("api/Employee/PostEmployee")]
        public HttpResponseMessage PostEmployee(Employee employee)
        {
            using(EmployeeDBContext dBContext = new EmployeeDBContext())
            {
                var entity = dBContext.Employees.FirstOrDefault(x => x.ID == employee.ID);
                if(entity == null)
                {
                    dBContext.Employees.Add(employee);
                    dBContext.SaveChanges();

                    var url = Url.Link("GetEmployeeByIdRouteName", new { stdId  = employee.ID});
                    var response = Request.CreateResponse(HttpStatusCode.Created);
                    response.Headers.Location = new Uri(url);

                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Employee already exists try with another Id");
                }
            }
        }
    }
}
