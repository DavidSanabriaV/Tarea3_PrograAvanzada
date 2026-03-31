using Microsoft.EntityFrameworkCore;
using Tarea3.Models;
using Tarea3.Data;

namespace Tarea3.Repositories
{
    public class CompraRepository : ICompraRepository
    {
        private readonly AppDbContext _context;

        public CompraRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Crear(Compra compra)
        {
            _context.Compras.Add(compra);
            _context.SaveChanges();
            return true;
        }

        public List<Compra> ObtenerPorCliente(string nombreCliente)
        {
            return _context.Compras
                .Include(c => c.Evento)
                .Where(c => c.NombreCliente == nombreCliente)
                .ToList();
        }
    }
}