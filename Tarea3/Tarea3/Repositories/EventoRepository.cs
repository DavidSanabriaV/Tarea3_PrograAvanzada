using Tarea3.Data;
using Tarea3.Models;

namespace Tarea3.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly AppDbContext _context;

        public EventoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Evento> ObtenerTodos()
        {
            return _context.Eventos.ToList();
        }

        public Evento? ObtenerPorId(int id)
        {
            return _context.Eventos.FirstOrDefault(e => e.Id == id);
        }

        public bool Crear(Evento evento)
        {
            _context.Eventos.Add(evento);
            _context.SaveChanges();
            return true;
        }
    }
}
