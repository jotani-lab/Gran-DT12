using Microsoft.AspNetCore.Mvc;
using ProyectoCore.Models;
using ProyectoCore.Services;

namespace ProyectoMinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipoController : ControllerBase
{
    private readonly ServiceEquipo _service;

    public EquipoController(ServiceEquipo service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public IActionResult Get() => Ok(_service.ObtenerTodos());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var res = _service.ObtenerPorId(id);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpPost("verificar")]
    public IActionResult Verificar([FromBody] string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return BadRequest(new { error = "El nombre del equipo es obligatorio." });

        return Ok(_service.Verificar(nombre));
    }

    [HttpPost]
    public IActionResult Create([FromBody] Equipo equipo)
    {
        try
        {
            if (!_service.Verificar(equipo?.Nombre ?? string.Empty))
                return BadRequest(new { error = "El nombre del equipo no es válido." });

            int id = _service.Crear(equipo);
            return CreatedAtAction(nameof(GetById), new { id }, equipo);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
