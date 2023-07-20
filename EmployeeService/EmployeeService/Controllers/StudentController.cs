using EmployeeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    [RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {
        //This controller is for the explanation of Attribute routing
        private IEnumerable<Student> StudentsList = new List<Student>{
            new Student(){Id = 1, Name = "Rohith"},
            new Student(){Id = 2, Name = "Vinay"},
            new Student(){Id = 3, Name = "Praharshini"},
            new Student(){Id = 4, Name = "Pravihith"}
        };

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, StudentsList.ToList());
        }

        public HttpResponseMessage GetStudent(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, StudentsList.Select(student => student.Id).FirstOrDefault());
        }

        //Here the below Http Method is very similar to the above method, If we didnt follow Convetion based routing, ASP.Net will not recognize the Above and below methods as they are very similar. To get rid of this kind of situations, where a particular Element have sub elements, ex : Students have subjects, Employees have jobs. we will get this kind of Ambiguity, we can achieve this by following convention based routing where we add {action} parameter to routeUriTemplate, but other way and most simple way is to Follow Attribute Routing, as like below

        [Route("GetSubjectsForStudentsById/{id}")]
        public HttpResponseMessage GetStudentInterests(int id)
        {
            var interestSubs = new List<string>();

            switch (id)
            {
                case 1: interestSubs = new List<string>() { "ASP.Net", "WebAPI", "SQL Server"}; break;
                case 2: interestSubs = new List<string>() { "ASP.Net", "React", "SQL Server" }; break;
                case 3: interestSubs = new List<string>() { "ASP.Net", "Azure", "DynamoDB" }; break;
                case 4: interestSubs = new List<string>() { "ASP.Net", "AWS", "AWS RedShift" }; break;
                default: interestSubs = new List<string>() { "ASP.Net", "Angular", "SQL Server" }; break;
            }

            return Request.CreateResponse(HttpStatusCode.OK, interestSubs);
        }

        //Optional URI parameters in Attribute Routing
        //Optional URI parameters in Attribute Routing can be done in two ways
        //First way
        //Here the default value for id will be 1, which we provided in the Method Parameter
        //here int is a constraint used to limit the parameter to be integer, like this we can apply alpha as well to constraint to strings
        [Route("~/Student/GetStudentByIdFirst/{stdId:int?}")]
        public HttpResponseMessage GetStudentByIdFirstWay(int stdId = 1)
        {
            return Request.CreateResponse(HttpStatusCode.OK, StudentsList.Where(x => x.Id == stdId).FirstOrDefault());
        }

        //Second way
        //here we can directly provide the default value in the Route Attribute itself
        [Route("GetStudentByIdSecond/{stdId:int=1}")]
        public HttpResponseMessage GetStudentByIdSecondWay(int stdId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, StudentsList.Where(x => x.Id == stdId).FirstOrDefault());
        }

        //Attribute Routing parameters constraints in Route
        [Route("GetStudentByName/{name:alpha=Rohith}")]
        public HttpResponseMessage GetStudentByName(string name)
        {
            return Request.CreateResponse(HttpStatusCode.OK, StudentsList.FirstOrDefault(x => x.Name.ToLower() == name.ToLower()));
        }

        //Attribute Routing parameters constraints in Route Example 2
        [Route("GetStudentByIdNew/{strId:int:min(1):max(4)}")]
        //[Route("GetStudentByIdNew/{strId:int:range(1,4)}")]
        public HttpResponseMessage GetStudentByIdNew(int strId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, StudentsList.FirstOrDefault(x => x.Id == strId));
        }

        //Attribute Routing parameters constraints in Route by using custom constraint
        [Route("GetStudentByNameUsingCustomConstraint/{strId:nonZero}")]
        public HttpResponseMessage GetStudentByNameUsingCustomConstraint(int strId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, StudentsList.FirstOrDefault(x => x.Id == strId));
        }
    }
}
