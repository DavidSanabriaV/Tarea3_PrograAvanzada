using Tarea3.Models;

namespace Tarea3.Repositories
{
    public interface ICompraRepository
    {
        List<Compra> ObtenerPorCliente(string nombreCliente);
        bool Crear(Compra compra);
    }
}