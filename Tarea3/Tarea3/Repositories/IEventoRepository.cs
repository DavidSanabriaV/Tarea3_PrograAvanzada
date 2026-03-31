using Tarea3.Models;

namespace Tarea3.Repositories
{
    public interface IEventoRepository
    {
        List<Evento> ObtenerTodos();
        Evento? ObtenerPorId(int id);
        bool Crear(Evento evento);
    }
}
