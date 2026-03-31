using Tarea3.Models;

namespace Tarea3.Services
{
    public interface ICompraService
    {
        bool CrearCompra(Compra compra);
        List<Compra> ObtenerPorCliente(string nombreCliente);
    }
}