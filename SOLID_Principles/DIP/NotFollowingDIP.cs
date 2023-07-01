using System;

namespace DIP
{
    //Dependency Inversion Principle states that Higher modules should not depend on lower modules, both should depend on Abstraction and Abstractions should not depend on Details,  Details should depend on Abstractions.
    //Here the below example shows how DIP is violating

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Salary { get; set; }
    }

    public class EmployeeDataAccessLogic
    {
        public Employee GetEmployeeDetails(int id)
        {
            return new Employee
            {
                Id = 100,
                Name = "Pravihith",
                Address = "Address",
                Salary = 1800000
            };
        }
    }

    public class DataAccessFactory
    {
        public static EmployeeDataAccessLogic GetEmployeeDataAccessObj()
        {
            return new EmployeeDataAccessLogic();
        }
    }

    public class EmployeeBusinessLogic
    {
        EmployeeDataAccessLogic employeeDataAccessLogic;

        public EmployeeBusinessLogic(EmployeeDataAccessLogic employeeDataAccessLogic)
        {
            this.employeeDataAccessLogic = employeeDataAccessLogic;
        }

        public Employee GetEmployeeDetails(int id)
        {
            return employeeDataAccessLogic.GetEmployeeDetails(id);
        }
    }
    public class NotFollowingDIP
    {
        //Here the Above classes are not following DIP, why becuase Higher module i.e EmployeeBusinessLogic which is dependent on lower module EmployeeDataAccessLogic which is a concrete class, but as per DIP, both should depend on Abstractions.
    }
}
