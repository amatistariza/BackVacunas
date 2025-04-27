namespace API.Domain.Models;

using System.ComponentModel.DataAnnotations;

public class Cuidador
{
    [Key]
    public int Id { get; set; } // Identificador único para el cuidador

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
    [StringLength(50)]
    public string Parentesco { get; set; } // Parentesco con la persona a cuidar (obligatorio)

    [Required]
    [EmailAddress]
    public string CorreoElectronico { get; set; } // Correo electrónico (obligatorio)

    [StringLength(10)]
    public string IndicativoTelefono { get; set; } // Indicativo del teléfono fijo (opcional)

    [StringLength(15)]
    public string TelefonoFijo { get; set; } // Teléfono fijo (opcional)

    [StringLength(15)]
    public string Celular { get; set; } // Celular (opcional)
}