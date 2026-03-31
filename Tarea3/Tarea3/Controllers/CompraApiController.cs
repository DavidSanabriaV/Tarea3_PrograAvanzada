using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarea3.Models;
using Tarea3.Services;

namespace Tarea3.Controllers.Api
{
    [Route("api/compras")]
    [ApiController]
    [Authorize]
    public class CompraApiController : ControllerBase
    {
        private readonly ICompraService _compraService;

        public CompraApiController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Compra compra)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new { message = "Datos inválidos.", errors });
            }

            if (!_compraService.CrearCompra(compra))
                return BadRequest(new { message = "No hay tiquetes disponibles o el evento no existe." });

            return CreatedAtAction(nameof(Historial),
                new { nombreCliente = compra.NombreCliente },
                new { compra.Id, compra.NombreCliente, compra.Cantidad, compra.Total, compra.FechaCompra });
        }

        [HttpGet("{nombreCliente}")]
        public IActionResult Historial(string nombreCliente)
        {
            if (string.IsNullOrEmpty(nombreCliente))
                return BadRequest(new { message = "Nombre de cliente no válido." });

            var compras = _compraService.ObtenerPorCliente(nombreCliente);

            if (compras == null || !compras.Any())
                return NotFound(new { message = "No se encontraron compras para este cliente." });

            return Ok(compras.Select(c => new
            {
                c.Id,
                c.NombreCliente,
                c.Cantidad,
                c.Total,
                c.FechaCompra,
                Evento = c.Evento == null ? null : new { c.Evento.Nombre, c.Evento.Fecha, c.Evento.Lugar }
            }));
        }
    }
}