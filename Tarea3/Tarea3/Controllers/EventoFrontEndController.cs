using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarea3.Constants;

[Route("eventos")]
[Authorize]
public class EventosFrontendController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View("~/Views/EventoFrontEnd/Index.cshtml");
    }

    [HttpGet("crear")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Crear()
    {
        return View("~/Views/EventoFrontEnd/Crear.cshtml");
    }
}