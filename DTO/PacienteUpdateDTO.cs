using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class PacienteUpdateDTO
    {
        // Id opcional en payload; si viene, se valida con la ruta
        public int Id { get; set; }

        // Campos opcionales en edición
        public string? Aseguradora { get; set; }
        public string? RegimenAfiliacion { get; set; }
        public string? PertenenciaEtnica { get; set; }
    }
}
