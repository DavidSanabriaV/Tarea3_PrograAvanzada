using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tarea3.Controllers
{
    [Route("compras")]
    [Authorize]
    public class ComprasFrontendController : Controller
    {
        [HttpGet("comprar/{id}")]
        public IActionResult Comprar(int id) => View("~/Views/CompraFrontend/Comprar.cshtml");

        [HttpGet("historial/{nombreCliente}")]
        public IActionResult Historial(string nombreCliente) => View("~/Views/CompraFrontend/Historial.cshtml");
    }
}