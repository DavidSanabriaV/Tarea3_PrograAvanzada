using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarea3.Constants;

namespace Tarea3.Controllers
{
    [Route("eventos")]
    [Authorize]
    public class EventosFrontendController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("crear")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Crear()
        {
            return View();
        }
    }
}
