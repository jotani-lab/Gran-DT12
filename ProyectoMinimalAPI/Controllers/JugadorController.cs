using Microsoft.AspNetCore.Mvc;
using ProyectoCore.Models;
using ProyectoCore.Services;

namespace ProyectoMinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JugadorController : ControllerBase
{
    private readonly ServiceJugador _service;
    public JugadorController(ServiceJugador service) { _service = service; }

    [HttpGet]
    public IActionResult Get() => Ok(_service.ObtenerTodos());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var res = _service.ObtenerPorId(id);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Jugador jugador)
    {
        try
        {
            int id = _service.Crear(jugador);
            return CreatedAtAction(nameof(GetById), new { id }, jugador);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
