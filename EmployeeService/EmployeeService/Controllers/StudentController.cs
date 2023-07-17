﻿using EmployeeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;

namespace EmployeeService.Controllers
{
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

        [Route("api/Student/GetSubjectsForStudentsById/{id}")]
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
    }
}