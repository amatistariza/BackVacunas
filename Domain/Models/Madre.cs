namespace API.Domain.Models;

using System.ComponentModel.DataAnnotations;

public class Madre
{
    [Key]
    public int Id { get; set; } // Identificador único para la madre

    [Required]
    [StringLength(20)]
    public string TipoIdentificacion { get; set; } // Tipo de identificación (ej. CC, TI, etc.)

    [Required]
    [StringLength(20)]
    public string NumeroIdentificacion { get; set; } // Número de identificación (obligatorio)

    [Required]
    [StringLength(50)]
    public string PrimerNombre { get; set; } // Primer nombre (obligatorio)

    [StringLength(50)]
    public string SegundoNombre { get; set; } // Segundo nombre (opcional)

    [Required]
    [StringLength(50)]
    public string PrimerApellido { get; set; } // Primer apellido (obligatorio)

    [StringLength(50)]
    public string SegundoApellido { get; set; } // Segundo apellido (opcional)

    [Required]
    [EmailAddress]
    public string CorreoElectronico { get; set; } // Correo electrónico (obligatorio)

    [StringLength(10)]
    public string IndicativoTelefono { get; set; } // Indicativo del teléfono fijo (opcional)

    [StringLength(15)]
    public string TelefonoFijo { get; set; } // Teléfono fijo (opcional)

    [StringLength(15)]
    public string Celular { get; set; } // Celular (opcional)

    [Required]
    [StringLength(50)]
    public string RegimenAfiliacion { get; set; } // Régimen de afiliación (obligatorio)

    [Required]
    [StringLength(50)]
    public string PertenenciaEtnica { get; set; } // Pertenencia étnica (obligatorio)

    [Required]
    public bool Desplazado { get; set; } // Indica si es desplazado (obligatorio)
}