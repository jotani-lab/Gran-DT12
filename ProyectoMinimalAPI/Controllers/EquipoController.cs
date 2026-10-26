using Microsoft.AspNetCore.Mvc;
using ProyectoCore.Models;
using ProyectoCore.Services;

namespace ProyectoMinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipoController : ControllerBase
{
    private readonly ServiceEquipo _service;
    public EquipoController(ServiceEquipo service) { _service = service; }

    [HttpGet]
    public IActionResult Get() => Ok(_service.ObtenerTodos());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var res = _service.ObtenerPorId(id);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Equipo equipo)
    {
        try
        {
            int id = _service.Crear(equipo);
            return CreatedAtAction(nameof(GetById), new { id }, equipo);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
