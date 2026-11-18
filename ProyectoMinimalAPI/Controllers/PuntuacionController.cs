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
        return Ok(_service.Validar(puntuacion));
    }

    [HttpPost]
    public IActionResult Post([FromBody] Puntuacion puntuacion)
    {
        try
        {
            var errores = _service.Validar(puntuacion);
            if (errores.Count > 0)
                return BadRequest(new { errores });

            bool ok = _service.RegistrarPuntuacion(puntuacion);
            return ok ? Ok(new { message = "Puntuación registrada con éxito." }) : BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
