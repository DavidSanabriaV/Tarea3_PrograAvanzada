using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tarea3.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreCliente { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal Total { get; set; }

        public DateTime FechaCompra { get; set; } = DateTime.Now;

        [ForeignKey(nameof(Evento))]
        public int EventoId { get; set; }

        public Evento? Evento { get; set; }

    }
}
