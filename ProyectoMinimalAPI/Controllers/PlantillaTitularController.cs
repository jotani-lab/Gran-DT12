using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ProyectoCore.Services;

namespace ProyectoMinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaTitularController : ControllerBase
{
    private readonly ServicePlantillaTitular _service;
    public PlantillaTitularController(ServicePlantillaTitular service) { _service = service; }

    [HttpPost("{plantillaId}/{jugadorId}")]
    public IActionResult Add(int plantillaId, int jugadorId)
    {
        try
        {
            bool ok = _service.AgregarTitular(plantillaId, jugadorId);
            return ok ? Ok(new { message = "Titular agregado correctamente." }) : Conflict();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (MySqlException ex) when (ex.Number == 1062)
        {
            return Conflict(new { error = "El jugador ya está asignado como titular en esa plantilla." });
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
            bool ok = _service.RemoverTitular(plantillaId, jugadorId);
            return ok ? Ok(new { message = "Titular removido correctamente." }) : NotFound();
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
