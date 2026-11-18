using Microsoft.AspNetCore.Mvc;
using ProyectoCore.DTOs;
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

    [HttpPost("verificar")]
    public IActionResult Verificar([FromBody] CrearJugadorRequest request)
    {
        var jugador = CrearEntidad(request);
        return Ok(_service.Validar(jugador));
    }

    [HttpPost]
    public IActionResult Create([FromBody] CrearJugadorRequest request)
    {
        try
        {
            var jugador = CrearEntidad(request);
            var errores = _service.Validar(jugador);
            if (errores.Count > 0)
                return BadRequest(new { errores });

            int id = _service.Crear(jugador);
            return CreatedAtAction(nameof(GetById), new { id }, jugador);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    private static Jugador CrearEntidad(CrearJugadorRequest request)
    {
        return new Jugador
        {
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Apodo = request.Apodo,
            FechaNacimiento = request.FechaNacimiento,
            EquipoId = request.EquipoId,
            PosicionId = request.PosicionId,
            Cotizacion = request.Cotizacion
        };
    }
}
