using Tarea3.Models;
using Tarea3.Repositories;

namespace Tarea3.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _repo;

        public EventoService(IEventoRepository repo)
        {
            _repo = repo;
        }

        public List<Evento> ObtenerTodos() => _repo.ObtenerTodos();

        public Evento? ObtenerPorId(int id) => _repo.ObtenerPorId(id);

        public bool Crear(Evento evento) => _repo.Crear(evento);
    }
}
