using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuthenticationWebAPI.Models
{
    public class EmployeeBL
    {
        public List<Employee> GetEmployees()
        {
            // In Real-time you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we hardcoded the data
            var employees = new List<Employee>();
            for(int i = 1; i <= 10; i++)
            {
                if (i > 5)
                {
                    employees.Add(new Employee()
                    {
                        ID = 100+i,
                        Salary = 100000+i,
                        Gender = "Male",
                        Dept = "Mechanical",
                        Name = "Employee"+i.ToString()
                    });
                }
                else
                {
                    employees.Add(new Employee()
                    {
                        ID = 100 + i,
                        Salary = 100000 + i,
                        Gender = "Female",
                        Dept = "Mechanical",
                        Name = "Employee" + i.ToString()
                    });
                }
            }

            return employees;
        }
    }
}