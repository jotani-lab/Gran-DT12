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

    [HttpGet("usuario/{usuarioId}")]
    public IActionResult GetByUsuario(int usuarioId)
    {
        var res = _service.ObtenerPorUsuarioId(usuarioId);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Plantilla plantilla)
    {
        try
        {
            int id = _service.Crear(plantilla);
            return Ok(new { id, message = "Plantilla creada correctamente." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
