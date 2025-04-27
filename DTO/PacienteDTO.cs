namespace API.Domain.DTOs
{
    using API.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PacienteDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime FechaAtencion { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoIdentificacion { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroIdentificacion { get; set; }

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
        public DateTime FechaNacimiento { get; set; }

        public bool EsquemaCompleto { get; set; }

        [Required]
        [StringLength(10)]
        public string Sexo { get; set; }

        [StringLength(20)]
        public string Genero { get; set; }

        [StringLength(50)]
        public string OrientacionSexual { get; set; }

        public int? EdadGestacionalSemanas { get; set; }

        [Required]
        [StringLength(50)]
        public string PaisNacimiento { get; set; }

        [Required]
        [StringLength(50)]
        public string EstatusMigratorio { get; set; }

        [Required]
        [StringLength(100)]
        public string LugarAtencionParto { get; set; }

        [Required]
        [StringLength(50)]
        public string RegimenAfiliacion { get; set; }

        [Required]
        [StringLength(100)]
        public string Aseguradora { get; set; }

        [Required]
        [StringLength(50)]
        public string PertenenciaEtnica { get; set; }

        [Required]
        public bool Desplazado { get; set; }

        [Required]
        public bool Discapacitado { get; set; }

        [Required]
        public bool Fallecido { get; set; }

        [Required]
        public bool VictimaConflictoArmado { get; set; }

        [Required]
        public bool EstudiaActualmente { get; set; }

        [Required]
        [StringLength(50)]
        public string PaisResidencia { get; set; }

        [Required]
        [StringLength(50)]
        public string DepartamentoResidencia { get; set; }

        [Required]
        [StringLength(50)]
        public string MunicipioResidencia { get; set; }

        [StringLength(50)]
        public string ComunaLocalidad { get; set; }

        [Required]
        [StringLength(50)]
        public string Area { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        [StringLength(10)]
        public string IndicativoTelefono { get; set; }

        [StringLength(15)]
        public string TelefonoFijo { get; set; }

        [StringLength(15)]
        public string Celular { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool AutorizaLlamadasTelefonicas { get; set; }

        [Required]
        public bool AutorizaEnvioCorreo { get; set; }
        public int MadreId { get; set; }

        public MadreDTO Madre { get; set; }
        public int CuidadorId { get; set; }

        public CuidadorDTO Cuidador { get; set; }

        public ICollection<AntecedenteDTO> Antecedentes { get; set; }

        public CondicionUsuariaDTO CondicionUsuaria { get; set; }

        public AntecedentesMedicosDTO AntecedentesMedicos { get; set; }

        //public EsquemaVacunaDTO EsquemaVacuna { get; set; }
    }

    public class MadreDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoIdentificacion { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroIdentificacion { get; set; }

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
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        [StringLength(10)]
        public string IndicativoTelefono { get; set; }

        [StringLength(15)]
        public string TelefonoFijo { get; set; }

        [StringLength(15)]
        public string Celular { get; set; }

        [Required]
        [StringLength(50)]
        public string RegimenAfiliacion { get; set; }

        [Required]
        [StringLength(50)]
        public string PertenenciaEtnica { get; set; }

        [Required]
        public bool Desplazado { get; set; }
    }

    public class CuidadorDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoIdentificacion { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroIdentificacion { get; set; }

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
        [StringLength(50)]
        public string Parentesco { get; set; }

        [Required]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        [StringLength(10)]
        public string IndicativoTelefono { get; set; }

        [StringLength(15)]
        public string TelefonoFijo { get; set; }

        [StringLength(15)]
        public string Celular { get; set; }
    }

    public class AntecedenteDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [StringLength(500)]
        public string ObservacionesEspeciales { get; set; }
    }

    public class CondicionUsuariaDTO
    {
        [Key]
        public int Id { get; set; } // Identificador único de la condición

        [Required]
        [StringLength(100)]
        public string Condicion { get; set; } // Condición de la usuaria (obligatorio)

        public bool Gestante { get; set; } // Indica si la usuaria está gestante

        public DateTime? FechaUltimaMenstruacion { get; set; } // Fecha de última menstruación (opcional)

        public int? SemanasGestacion { get; set; } // Semanas de gestación (opcional)

        public DateTime? FechaProbableParto { get; set; } // Fecha probable de parto (opcional)

        public int CantidadEmbarazosPrevios { get; set; } // Cantidad de embarazos previos
    }

    public class AntecedentesMedicosDTO
    {
        [Key]
        public int Id { get; set; } // Identificador único del antecedente médico

        [Required]
        public bool ContraindicacionVacunacion { get; set; } // ¿Sufre o ha sufrido algo que contraindique la vacunación?

        [StringLength(500)]
        public string DetalleContraindicacion { get; set; } // Detalle de la contraindicación (opcional)

        [Required]
        public bool ReaccionBiologicos { get; set; } // ¿Ha presentado reacción moderada o severa a biológicos anteriores?

        [StringLength(500)]
        public string DetalleReaccion { get; set; } // Detalle de la reacción a biológicos (opcional)
    }

    public class EsquemaVacunaDTO
    {
        [Key]
        public int Id { get; set; } // Identificador único del esquema

        [Required]
        [StringLength(50)]
        public string TipoCarnet { get; set; } // Tipo de carnet

        [Required]
        [StringLength(100)]
        public string Responsable { get; set; } // Responsable del esquema (vacunador)

        [Required]
        public bool RegistradoPAI { get; set; } // Indica si fue registrado en el PAI

        [StringLength(500)]
        public string? MotivoNoIngreso { get; set; } // Motivo de no ingreso (opcional)

        [StringLength(500)]
        public string? Observaciones { get; set; } // Observaciones adicionales (opcional)

        // Relación 1:N con Detalles
        public ICollection<EsquemaVacunacionDetalle> Detalles { get; set; } // Lista de detalles del esquema
    }
}
