using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
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
        try
        {
            var res = _service.ObtenerPorId(id);
            return res == null ? NotFound() : Ok(res);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("borrar")]
    public IActionResult Borrar([FromBody] BorrarPorIdRequest request)
    {
        if (request == null)
            return BadRequest(new { error = "Los datos del jugador son obligatorios." });

        try
        {
            var jugador = BorrarEntidad(request);
            var filasEliminadas = _service.BorrarPorId(jugador);
            return filasEliminadas == 0 ? NotFound() : Ok(filasEliminadas);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (MySqlException ex) when (ex.Number == 1451)
        {
            return Conflict(new { error = "No se puede eliminar el jugador porque tiene puntuaciones o plantillas relacionadas." });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] CrearJugadorRequest request)
    {
        if (request == null)
            return BadRequest(new { error = "Los datos del jugador son obligatorios." });

        try
        {
            var jugador = CrearEntidad(request);
            var errores = _service.Validar(jugador);
            if (errores.Count > 0)
                return BadRequest(new { errores });

            int id = _service.Crear(jugador);
            jugador.Id = id;
            return CreatedAtAction(nameof(GetById), new { id }, jugador);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (MySqlException ex) when (ex.Number == 1452)
        {
            return BadRequest(new { error = "El equipo o la posición indicada no existe." });
        }
        catch (MySqlException ex) when (ex.Number == 1062)
        {
            return Conflict(new { error = "El jugador ya está registrado." });
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

    private static Jugador BorrarEntidad(BorrarPorIdRequest request)
    {
        return new Jugador
        {
            Id = request.Id
        };
    }
}
