using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarea3.Constants;
using Tarea3.Models;
using Tarea3.Services;

namespace Tarea3.Controllers
{
    [Route("api/eventos")]
    [ApiController]
    [Authorize]
    public class EventosApiController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosApiController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var eventos = _eventoService.ObtenerTodos()
                .Select(e => new{e.Id,e.Nombre,e.Fecha,e.Lugar,e.Precio,e.CantidadDisponible });

            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var evento = _eventoService.ObtenerPorId(id);
            if (evento == null)
                return NotFound(new { message = "Evento no encontrado." });

            return Ok(new
            {evento.Id,evento.Nombre,evento.Fecha,evento.Lugar,evento.Precio,evento.CantidadDisponible});
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Crear([FromBody] Evento evento)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Datos invalidos.", errors });
            }

            _eventoService.Crear(evento);
            return CreatedAtAction(nameof(ObtenerPorId),
                new { id = evento.Id },
                new { evento.Id, evento.Nombre });
        }
    }
}