using Microsoft.AspNetCore.Mvc;
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
        bool ok = _service.AgregarTitular(plantillaId, jugadorId);
        return ok ? Ok() : BadRequest();
    }

    [HttpDelete("{plantillaId}/{jugadorId}")]
    public IActionResult Remove(int plantillaId, int jugadorId)
    {
        bool ok = _service.RemoverTitular(plantillaId, jugadorId);
        return ok ? Ok() : BadRequest();
    }
}
