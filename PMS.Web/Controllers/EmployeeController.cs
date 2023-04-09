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

        public async Task<IActionResult> Index(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("NotFound", "Home");
            }
            var employee = await this.employeeService.GetEmployeeById(id);
            return View(employee);
        }

        public IActionResult GetAll()
        {
            var employees = this.employeeService.GetAllEmployees();
            return View(employees);
        }
    }
}