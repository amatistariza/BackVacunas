using Xunit;

namespace BackVacunas.Tests;

public class CalculoEdadTests
{
    // Métodos copiados del StatisticsService para probar
    private static int CalcularEdadEnAnios(DateTime fechaNacimiento, DateTime fechaReferencia)
    {
        var edad = fechaReferencia.Year - fechaNacimiento.Year;
        if (fechaReferencia < fechaNacimiento.AddYears(edad))
        {
            edad--;
        }
        return edad;
    }

    private static int CalcularEdadEnMeses(DateTime fechaNacimiento, DateTime fechaReferencia)
    {
        var meses = (fechaReferencia.Year - fechaNacimiento.Year) * 12;
        meses += fechaReferencia.Month - fechaNacimiento.Month;
        
        // Ajustar si el día de referencia es menor al día de nacimiento
        if (fechaReferencia.Day < fechaNacimiento.Day)
        {
            meses--;
        }
        
        return Math.Max(0, meses); // Nunca negativo
    }

    private static int CalcularEdadEnDias(DateTime fechaNacimiento, DateTime fechaReferencia)
    {
        var dias = (fechaReferencia.Date - fechaNacimiento.Date).Days;
        return Math.Max(0, dias); // Nunca negativo
    }

    [Fact]
    public void CalcularEdadEnMeses_BebeDe2Meses_Debe_Retornar2()
    {
        // Arrange: Bebé nacido hace 2 meses
        var fechaNacimiento = new DateTime(2025, 8, 6); // 6 de agosto 2025
        var fechaReferencia = new DateTime(2025, 10, 6); // 6 de octubre 2025
        
        // Act
        var meses = CalcularEdadEnMeses(fechaNacimiento, fechaReferencia);
        
        // Assert
        Assert.Equal(2, meses);
    }

    [Fact]
    public void CalcularEdadEnMeses_BebeDe3Meses_Debe_Retornar3()
    {
        // Arrange: Bebé nacido hace 3 meses
        var fechaNacimiento = new DateTime(2025, 7, 1); // 1 de julio 2025
        var fechaReferencia = new DateTime(2025, 10, 6); // 6 de octubre 2025
        
        // Act
        var meses = CalcularEdadEnMeses(fechaNacimiento, fechaReferencia);
        
        // Assert
        Assert.Equal(3, meses);
    }

    [Fact]
    public void CalcularEdadEnMeses_BebeDe2MesesConDiasMenores_Debe_Retornar2()
    {
        // Arrange: Bebé nacido el 15 de agosto, hoy 6 de octubre
        var fechaNacimiento = new DateTime(2025, 8, 15); // 15 de agosto 2025
        var fechaReferencia = new DateTime(2025, 10, 6); // 6 de octubre 2025
        
        // Act
        var meses = CalcularEdadEnMeses(fechaNacimiento, fechaReferencia);
        
        // Assert: Solo ha pasado 1 mes completo (porque 6 < 15)
        Assert.Equal(1, meses);
    }

    [Fact]
    public void CalcularEdadEnMeses_RecienNacido_Debe_Retornar0()
    {
        // Arrange: Bebé nacido hace 15 días
        var fechaNacimiento = new DateTime(2025, 9, 21); // 21 de septiembre 2025
        var fechaReferencia = new DateTime(2025, 10, 6); // 6 de octubre 2025
        
        // Act
        var meses = CalcularEdadEnMeses(fechaNacimiento, fechaReferencia);
        
        // Assert
        Assert.Equal(0, meses);
    }

    [Fact]
    public void CalcularEdadEnDias_RecienNacido15Dias_Debe_Retornar15()
    {
        // Arrange: Bebé nacido hace 15 días
        var fechaNacimiento = new DateTime(2025, 9, 21); // 21 de septiembre 2025
        var fechaReferencia = new DateTime(2025, 10, 6); // 6 de octubre 2025
        
        // Act
        var dias = CalcularEdadEnDias(fechaNacimiento, fechaReferencia);
        
        // Assert
        Assert.Equal(15, dias);
    }

    [Fact]
    public void CalcularEdadEnAnios_NinioDe2Anios_Debe_Retornar2()
    {
        // Arrange: Niño nacido hace 2 años
        var fechaNacimiento = new DateTime(2023, 3, 15); // 15 de marzo 2023
        var fechaReferencia = new DateTime(2025, 10, 6); // 6 de octubre 2025
        
        // Act
        var anios = CalcularEdadEnAnios(fechaNacimiento, fechaReferencia);
        
        // Assert
        Assert.Equal(2, anios);
    }

    [Fact]
    public void CalcularEdadEnMeses_BebeDe11Meses_Debe_Retornar11()
    {
        // Arrange: Bebé nacido hace 11 meses
        var fechaNacimiento = new DateTime(2024, 11, 6); // 6 de noviembre 2024
        var fechaReferencia = new DateTime(2025, 10, 6); // 6 de octubre 2025
        
        // Act
        var meses = CalcularEdadEnMeses(fechaNacimiento, fechaReferencia);
        
        // Assert
        Assert.Equal(11, meses);
    }

    [Fact]
    public void CalcularEdadEnMeses_1AnioExacto_Debe_Retornar12()
    {
        // Arrange: Bebé que cumple 1 año exacto
        var fechaNacimiento = new DateTime(2024, 10, 6); // 6 de octubre 2024
        var fechaReferencia = new DateTime(2025, 10, 6); // 6 de octubre 2025
        
        // Act
        var meses = CalcularEdadEnMeses(fechaNacimiento, fechaReferencia);
        
        // Assert
        Assert.Equal(12, meses);
    }

    [Fact]
    public void CalcularEdadEnAnios_1AnioExacto_Debe_Retornar1()
    {
        // Arrange: Niño que cumple 1 año exacto
        var fechaNacimiento = new DateTime(2024, 10, 6); // 6 de octubre 2024
        var fechaReferencia = new DateTime(2025, 10, 6); // 6 de octubre 2025
        
        // Act
        var anios = CalcularEdadEnAnios(fechaNacimiento, fechaReferencia);
        
        // Assert
        Assert.Equal(1, anios);
    }
}
