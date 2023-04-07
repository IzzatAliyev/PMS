using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;

namespace PMS.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // var employee = new EmployeeViewModel()
            // {
            //     Name = "anton",
            //     Position = "master",
            //     Email = "employeeDb.Email",
            //     Description = "employeeDb.Description",
            //     PhoneNumber = "employeeDb.PhoneNumber"
            // };
            // await this.employeeService.CreateEmployee(employee);
            // await this.employeeService.UpdateEmployeeById(2, employee);
            // await this.employeeService.DeleteEmployeeById(2);
            // var empl = await this.employeeService.GetEmployeeById(3);
            // Console.WriteLine(empl.Name);
            return View();
        }

        public IActionResult GetAll()
        {
            var employees = this.employeeService.GetAllEmployees();
            foreach(var emp in employees)
            {
                Console.WriteLine(emp.Name);
            }
            return View(employees);
        }
    }
}