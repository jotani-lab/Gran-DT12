using Microsoft.AspNetCore.Mvc;
using ProyectoCore.Services;

namespace ProyectoMinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaSuplenteController : ControllerBase
{
    private readonly ServicePlantillaSuplente _service;
    public PlantillaSuplenteController(ServicePlantillaSuplente service) { _service = service; }

    [HttpPost("{plantillaId}/{jugadorId}")]
    public IActionResult Add(int plantillaId, int jugadorId)
    {
        try
        {
            bool ok = _service.AgregarSuplente(plantillaId, jugadorId);
            return ok ? Ok(new { message = "Suplente agregado correctamente." }) : Conflict();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{plantillaId}/{jugadorId}")]
    public IActionResult Remove(int plantillaId, int jugadorId)
    {
        try
        {
            bool ok = _service.RemoverSuplente(plantillaId, jugadorId);
            return ok ? Ok(new { message = "Suplente removido correctamente." }) : NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
