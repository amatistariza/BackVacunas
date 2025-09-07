using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class CondicionUsuariaCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string Condicion { get; set; }

        public bool Gestante { get; set; }

        public DateTime? FechaUltimaMenstruacion { get; set; }

        public int SemanasGestacion { get; set; }

        public int CantidadEmbarazosPrevios { get; set; }

        public DateTime? FechaProbableParto { get; set; }
    }
}
