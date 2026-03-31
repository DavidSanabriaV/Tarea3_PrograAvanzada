using Tarea3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Tarea3.Data
{
    public class AppDbContext : IdentityDbContext<Persona, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Compra> Compras { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Persona> Personas => Users;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Evento)
                .WithMany(e => e.Compras)
                .HasForeignKey(c => c.EventoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Persona>()
                .HasIndex(p => p.Cedula)
                .IsUnique();
        }
        }
}
