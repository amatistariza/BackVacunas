#nullable enable annotations
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegistroVacunacionDTO
    {
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        [Required]
        public int VacunaId { get; set; }

        [Required]
        public DateTime FechaAplicacion { get; set; }

        [Required]
        public int NumeroDosis { get; set; }

    [StringLength(100)]
    public string? LugarAplicacion { get; set; }

    [StringLength(50)]
    public string? Lote { get; set; }

    public string? VacunaNombre { get; set; }

    public string? PacienteNombre { get; set; }

        public DateTime FechaRegistro { get; set; }

    public string? Observaciones { get; set; }
    }

    public class RegistroVacunacionCreateDTO
    {
        [Required]
        public int PacienteId { get; set; }

        [Required]
        public int VacunaId { get; set; }

        [Required]
        public DateTime FechaAplicacion { get; set; }

        [Required]
        public int NumeroDosis { get; set; }

    [StringLength(100)]
    public string? LugarAplicacion { get; set; }

    [StringLength(50)]
    public string? Lote { get; set; }

    public string? Observaciones { get; set; }
    }
}
