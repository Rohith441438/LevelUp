using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP
{
    public interface IEmployeeDataAccessLogic
    {
        Employee GetEmployeeAccessData(int id);
    }

    public class EmployeeDataAccessLogicNew : IEmployeeDataAccessLogic
    {
        public Employee GetEmployeeAccessData(int id)
        {
            return new Employee
            {
                Id = 100,
                Name = "Pravihith",
                Address = "Rohith",
                Salary = 2000000
            };
        }
    }

    public class DataAccessFactoryNew
    {
        public static IEmployeeDataAccessLogic GetEmployeeAccessLogic()
        {
            return new EmployeeDataAccessLogicNew();
        }
    }

    public class EmployeeBusinessLogicNew
    {
        IEmployeeDataAccessLogic _employeeDataAccessLogic;

        public EmployeeBusinessLogicNew()
        {
            _employeeDataAccessLogic = DataAccessFactoryNew.GetEmployeeAccessLogic();
        }

        public Employee getEmployeeData(int id)
        {
            return _employeeDataAccessLogic.GetEmployeeAccessData(id);
        }
    }
    class FollowingDIP
    {
        //Here the Higher modules(EmployeeBusinessLogicNew) and lower modules(EmployeeDataAccessLogicNew) are not dependent on each other, they are depending on Abstract class IEmployeeDataAccessLogic. and Abstractions(IEmployeeDataAccessLogic) is not dependent on Details(EmployeeDaataAccessLogicNew), Details depend on Abstractions.
    }
}
