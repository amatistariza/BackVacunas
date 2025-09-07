using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class AntecedentesMedicosCreateDTO
    {
        public bool ContraindicacionVacunacion { get; set; }

        [StringLength(500)]
        public string DetalleContraindicacion { get; set; }

        public bool ReaccionBiologicos { get; set; }

        [StringLength(500)]
        public string DetalleReaccion { get; set; }
    }
}
