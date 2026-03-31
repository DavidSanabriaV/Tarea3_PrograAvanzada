using Microsoft.AspNetCore.Mvc;

namespace Tarea3.Controllers
{
    [Route("auth-frontend")]
    public class AuthFrontendController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("~/Views/Auth/Login.cshtml");
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View("~/Views/Auth/Register.cshtml");
        }
    }
}