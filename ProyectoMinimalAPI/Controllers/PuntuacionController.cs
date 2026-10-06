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

    [HttpPost]
    public IActionResult Post([FromBody] Puntuacion puntuacion)
    {
        try
        {
            bool ok = _service.RegistrarPuntuacion(puntuacion);
            return ok ? Ok(new { message = "Puntuación registrada con éxito." }) : BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
