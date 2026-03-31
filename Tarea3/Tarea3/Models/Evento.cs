using System.ComponentModel.DataAnnotations;

namespace Tarea3.Models
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string Lugar { get; set; }

        [Required]
        public decimal Precio { get; set; }

        public int CantidadDisponible { get; set; }

        public List<Compra> Compras { get; set; } = new();
    }
}
