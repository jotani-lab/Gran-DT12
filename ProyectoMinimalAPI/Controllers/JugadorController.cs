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

    [HttpGet("verificar")]
    public IActionResult Verificar(
        [FromQuery] string nombre,
        [FromQuery] string apellido,
        [FromQuery] string apodo,
        [FromQuery] int equipoId,
        [FromQuery] DateOnly fechaNacimiento,
        [FromQuery] int posicionId,
        [FromQuery] decimal cotizacion)
    {
        return Ok(_service.Verificar(nombre, apellido, apodo, equipoId, fechaNacimiento, posicionId, cotizacion));
    }

    [HttpPost]
    public IActionResult Create([FromBody] Jugador jugador)
    {
        try
        {
            if (!_service.Verificar(
                jugador.Nombre,
                jugador.Apellido,
                jugador.Apodo,
                jugador.EquipoId,
                jugador.FechaNacimiento,
                jugador.PosicionId,
                jugador.Cotizacion))
            {
                return BadRequest(new { error = "Los datos del jugador no son válidos." });
            }

            int id = _service.Crear(jugador);
            return CreatedAtAction(nameof(GetById), new { id }, jugador);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
