using Microsoft.AspNetCore.Mvc;
using ProyectoCore.Models;
using ProyectoCore.Services;

namespace ProyectoMinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PuntuacionController : ControllerBase
{
    private readonly ServicePuntuacion _service;
    public PuntuacionController(ServicePuntuacion service) { _service = service; }

    [HttpGet("jugador/{jugadorId}")]
    public IActionResult GetByJugador(int jugadorId)
    {
        try
        {
            var res = _service.ObtenerPorJugadorId(jugadorId);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("verificar")]
    public IActionResult Verificar([FromBody] Puntuacion puntuacion)
    {
        if (puntuacion == null)
            return BadRequest(new { error = "La puntuación es obligatoria." });

        return Ok(_service.Verificar(puntuacion));
    }

    [HttpPost]
    public IActionResult Post([FromBody] Puntuacion puntuacion)
    {
        try
        {
            if (!_service.Verificar(puntuacion))
                return BadRequest(new { error = "La puntuación no es válida." });

            bool ok = _service.RegistrarPuntuacion(puntuacion);
            return ok ? Ok(new { message = "Puntuación registrada con éxito." }) : BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
