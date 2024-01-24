using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zancudo.Entidades
{
    public class Disfraz
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Requerido.")]
        [StringLength(25, ErrorMessage = "Maximo 25 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Requerido.")]
        public int TipoDisfrazId { get; set; }
        [ForeignKey("TipoDisfrazId")]
        public TipoDisfraz TipoDisfraz { get; set; }
        public bool EstaBorrado { get; set; }
        public HashSet<ClienteDisfraz> ClientesDisfraces { get; set; }
    }
}
