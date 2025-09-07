using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class PacienteCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string PrimerNombre { get; set; }

        [StringLength(50)]
        public string SegundoNombre { get; set; }

        [Required]
        [StringLength(50)]
        public string PrimerApellido { get; set; }

        [StringLength(50)]
        public string SegundoApellido { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoIdentificacion { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroIdentificacion { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }


        [Required]
        [StringLength(10)]
        public string Sexo { get; set; }

        [StringLength(50)]
        public string PaisNacimiento { get; set; }

        [StringLength(50)]
        public string PaisResidencia { get; set; }

        [StringLength(50)]
        public string DepartamentoResidencia { get; set; }

        [StringLength(50)]
        public string MunicipioResidencia { get; set; }

        [StringLength(50)]
        public string ComunaLocalidad { get; set; }

        [StringLength(50)]
        public string Area { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        [StringLength(15)]
        public string TelefonoFijo { get; set; }

        [StringLength(15)]
        public string Celular { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string PertenenciaEtnica { get; set; }

        [StringLength(50)]
        public string OrientacionSexual { get; set; }

        public bool Discapacitado { get; set; }

        public bool VictimaConflictoArmado { get; set; }

        public bool Desplazado { get; set; }

        public bool EstudiaActualmente { get; set; }

        [StringLength(50)]
        public string RegimenAfiliacion { get; set; }

        [StringLength(100)]
        public string Aseguradora { get; set; }

        [StringLength(50)]
        public string EstatusMigratorio { get; set; }

        public bool Fallecido { get; set; }

        public DateTime FechaAtencion { get; set; }


        public int EdadGestacionalSemanas { get; set; }

        public bool EsquemaCompleto { get; set; }

        public bool AutorizaLlamadasTelefonicas { get; set; }

        public bool AutorizaEnvioCorreo { get; set; }

        // Opcionales - referencias a madre y cuidador
        public int? MadreId { get; set; }
        public int? CuidadorId { get; set; }

        // Entidades relacionadas (sin IDs)
        public CondicionUsuariaCreateDTO CondicionUsuaria { get; set; }
        public AntecedentesMedicosCreateDTO AntecedentesMedicos { get; set; }
        public ICollection<AntecedenteCreateDTO> Antecedentes { get; set; }
    }
}
