using Tarea3.Models;
using Tarea3.Repositories;

namespace Tarea3.Services
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _repository;
        private readonly IEventoRepository _eventoRepository;

        public CompraService(ICompraRepository repository, IEventoRepository eventoRepository)
        {
            _repository = repository;
            _eventoRepository = eventoRepository;
        }

        public bool CrearCompra(Compra compra)
        {
            var evento = _eventoRepository.ObtenerPorId(compra.EventoId);

            if (evento == null)
                return false;

            if (evento.CantidadDisponible < compra.Cantidad)
                return false;

            compra.Total = evento.Precio * compra.Cantidad;
            compra.FechaCompra = DateTime.Now;
            evento.CantidadDisponible -= compra.Cantidad;

            _repository.Crear(compra);
            return true;
        }

        public List<Compra> ObtenerPorCliente(string nombreCliente)
            => _repository.ObtenerPorCliente(nombreCliente);
    }
}