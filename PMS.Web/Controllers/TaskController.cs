using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMS.Web.Controllers
{
    [Authorize]
    [Route("tasks")]
    public class TaskController : Controller
    {
         public IActionResult Index()
        {
            return View();
        }   
    }
}