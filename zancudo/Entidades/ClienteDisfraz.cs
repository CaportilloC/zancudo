using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zancudo.Entidades
{
    public class ClienteDisfraz
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Requerido.")]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        public int DisfrazId { get; set; }
        [ForeignKey("DisfrazId")]
        public Disfraz Disfraz { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        public DateTime? FechaInicio { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        public DateTime? FechaFin { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        public int TipoPagoId { get; set; }
        [ForeignKey("TipoPagoId")]
        public TipoPago TipoPago { get; set; }
        public bool EstaBorrado { get; set; }

        [NotMapped]
        public int? DiasArriendo
        {
            get
            {
                if (!FechaInicio.HasValue || !FechaFin.HasValue)
                {
                    return null;
                }

                if (FechaFin.Value < FechaInicio.Value)
                {
                    return null;
                }

                var diasArriendo = (int)(FechaFin.Value - FechaInicio.Value).TotalDays;

                return diasArriendo;
            }
        }
    }
}
