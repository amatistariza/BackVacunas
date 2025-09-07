using API.Domain.IServices;
using API.Domain.Models;
using API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EsquemaVacunacionController : ControllerBase
{
    private readonly IEsquemaVacunacionService _esquemaService;
    public EsquemaVacunacionController(IEsquemaVacunacionService esquemaService)
    {
        _esquemaService = esquemaService;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarEsquema([FromBody] EsquemaVacunacionCreateDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var esquema = new EsquemaVacunacion
            {
                TipoCarnet = dto.TipoCarnet,
                Responsable = dto.Responsable,
                RegistradoPAI = dto.RegistradoPAI,
                MotivoIngreso = dto.MotivoIngreso,
                Observaciones = dto.Observaciones,
                PacienteId = dto.PacienteId,
                VacunaId = dto.VacunaId,
                NumeroDeDosis = dto.NumeroDeDosis,
                FechaDosisAplicada = dto.FechaDosisAplicada,
                ViaDeAdministracion = dto.ViaDeAdministracion,
                SitioDeAplicacion = dto.SitioDeAplicacion,
                Lote = dto.Lote,
                Detalles = dto.Detalles.Select(d => new EsquemaVacunacionDetalle
                {
                    VacunaId = d.VacunaId,
                    CantidadUtilizadaVacuna = d.CantidadUtilizadaVacuna,
                    SueroId = d.SueroId,
                    CantidadUtilizadaSuero = d.CantidadUtilizadaSuero,
                    DiluyenteId = d.DiluyenteId,
                    CantidadUtilizadaDiluyente = d.CantidadUtilizadaDiluyente,
                    JeringaId = d.JeringaId,
                    CantidadUtilizadaJeringa = d.CantidadUtilizadaJeringa
                }).ToList()
            };

            await _esquemaService.RegistrarEsquemaAsync(esquema);
            return Ok(new { mensaje = "Esquema registrado correctamente.", esquemaId = esquema.Id });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            var inner = ex.InnerException?.Message;
            return StatusCode(500, new { mensaje = "Error inesperado al registrar esquema.", error = ex.Message, inner });
        }
    }

    [HttpGet("{esquemaId}")]
    public async Task<IActionResult> GetEsquemaConDetalles(int esquemaId)
    {
        try
        {
            var esquema = await _esquemaService.GetEsquemaConDetallesAsync(esquemaId);
            if (esquema == null) return NotFound(new { mensaje = "Esquema no encontrado." });

            var paciente = esquema.Paciente;
            var vacuna = esquema.Vacuna;

            var response = new {
                esquema.Id,
                esquema.TipoCarnet,
                esquema.Responsable,
                esquema.RegistradoPAI,
                esquema.MotivoIngreso,
                esquema.Observaciones,
                esquema.PacienteId,
                Paciente = paciente != null ? new {
                    paciente.Id,
                    paciente.NumeroIdentificacion,
                    paciente.PrimerNombre,
                    paciente.PrimerApellido,
                    paciente.Sexo,
                    paciente.FechaNacimiento
                } : null,
                esquema.VacunaId,
                Vacuna = vacuna != null ? new {
                    vacuna.Id,
                    vacuna.Nombre,
                    vacuna.NumeroDosis,
                    vacuna.IntervaloSemanas
                } : null,
                esquema.NumeroDeDosis,
                esquema.FechaDosisAplicada,
                esquema.FechaProximaDosis,
                esquema.ViaDeAdministracion,
                esquema.SitioDeAplicacion,
                esquema.Lote,
                Detalles = esquema.Detalles.Select(d => new {
                    d.Id,
                    d.VacunaId,
                    d.CantidadUtilizadaVacuna,
                    d.SueroId,
                    d.DiluyenteId,
                    d.JeringaId
                })
            };
            return Ok(response);
        }
        catch (Exception ex)
        {
            var inner = ex.InnerException?.Message;
            return StatusCode(500, new { mensaje = "Error inesperado.", error = ex.Message, inner });
        }
    }
}
