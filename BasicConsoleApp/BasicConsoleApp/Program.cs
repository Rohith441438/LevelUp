using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Here we will see how we can consume an Web API in console App
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", Convert.ToBase64String(Encoding.Default.GetBytes("AdminUser:AdGHkl")));

            var result = client.GetAsync(new Uri("https://localhost:44357/api/Employee/GetEmployees")).Result;

            if(result.IsSuccessStatusCode)
            {
                Console.WriteLine("Retreived the EmployeesList ");
                var jsonResult = result.Content.ReadAsStringAsync().Result;

                //Need to serialize into empList

                var empList = JsonConvert.DeserializeObject<List<Employee>>(jsonResult);

                foreach(Employee emp in empList)
                {
                    Console.WriteLine("Name : " + emp.Name + " Gender : " + emp.Gender + " Dept : " + emp.Dept + " Salary : " + emp.Salary);
                }
            }
        }
    }

    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Dept { get; set; }
        public int Salary { get; set; }
    }
}