using Tarea3.Models;

namespace Tarea3.Services
{
    public interface IEventoService
    {
        List<Evento> ObtenerTodos();
        Evento? ObtenerPorId(int id);
        bool Crear(Evento evento);
    }
}
