using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMS.Web.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    public class AuthController : Controller
    {

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
    }
}