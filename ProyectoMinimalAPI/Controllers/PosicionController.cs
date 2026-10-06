using Microsoft.AspNetCore.Mvc;
using ProyectoCore.Services;

namespace ProyectoMinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PosicionController : ControllerBase
{
    private readonly ServicePosicion _service;
    public PosicionController(ServicePosicion service) { _service = service; }

    [HttpGet]
    public IActionResult Get() => Ok(_service.ObtenerTodas());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var res = _service.ObtenerPorId(id);
        return res == null ? NotFound() : Ok(res);
    }
}
