using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
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
        catch (MySqlException ex) when (ex.Number == 1062)
        {
            return Conflict(new { error = "El jugador ya está asignado como suplente en esa plantilla." });
        }
        catch (MySqlException ex) when (ex.Number == 1452)
        {
            return BadRequest(new { error = "La plantilla o el jugador indicado no existe." });
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
        catch (MySqlException ex) when (ex.Number == 1452)
        {
            return BadRequest(new { error = "La plantilla o el jugador indicado no existe." });
        }
    }
}
