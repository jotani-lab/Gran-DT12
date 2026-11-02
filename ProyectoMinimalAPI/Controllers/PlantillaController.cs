using Microsoft.AspNetCore.Mvc;
using ProyectoCore.Models;
using ProyectoCore.Services;

namespace ProyectoMinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaController : ControllerBase
{
    private readonly ServicePlantilla _service;
    public PlantillaController(ServicePlantilla service) { _service = service; }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var res = _service.ObtenerPorId(id);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpGet("usuario/{usuarioId}")]
    public IActionResult GetByUsuario(int usuarioId)
    {
        var res = _service.ObtenerPorUsuarioId(usuarioId);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpGet("{id}/puntaje-fecha/{nroFecha}")]
    public IActionResult PuntajeFecha(int id, int nroFecha)
    {
        var plantilla = _service.ObtenerPorId(id);
        if (plantilla == null)
            return NotFound();

        try
        {
            return Ok(_service.PuntajeFecha(plantilla, nroFecha));
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("verificar")]
    public IActionResult Verificar([FromBody] Plantilla plantilla)
    {
        if (plantilla == null)
            return BadRequest(new { error = "La plantilla es obligatoria." });

        return Ok(_service.Verificar(plantilla));
    }

    [HttpPost]
    public IActionResult Create([FromBody] Plantilla plantilla)
    {
        try
        {
            if (!_service.Verificar(plantilla))
                return BadRequest(new { error = "La plantilla no es válida." });

            int id = _service.Crear(plantilla);
            return Ok(new { id, message = "Plantilla creada correctamente." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
