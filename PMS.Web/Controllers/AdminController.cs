using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace PMS.Web.Controllers
{
    [Authorize]
    [Route("admin")]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("projects")]
        public IActionResult Projects()
        {
            return View();
        }
                
        [HttpGet("employees")]
        public IActionResult Employees()
        {
            return View();
        }
        
    }
}