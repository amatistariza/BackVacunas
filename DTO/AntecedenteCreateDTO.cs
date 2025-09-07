using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class AntecedenteCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public DateTime FechaRegistro { get; set; }

        [StringLength(500)]
        public string ObservacionesEspeciales { get; set; }
    }
}
