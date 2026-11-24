using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ProyectoCore.DTOs;
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

    [HttpGet("nombre/{nombre}")]
    public IActionResult GetByNombre(string nombre)
    {
        try
        {
            var equipo = _service.ObtenerPorNombre(nombre);
            return equipo == null ? NotFound() : Ok(equipo);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] CrearEquipoRequest request)
    {
        try
        {
            var equipo = CrearEntidad(request);
            var errores = _service.Validar(equipo);
            if (errores.Count > 0)
                return BadRequest(new { errores });

            int id = _service.Crear(equipo);
            equipo.Id = id;
            return CreatedAtAction(nameof(GetById), new { id }, equipo);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (MySqlException ex) when (ex.Number == 1062)
        {
            return Conflict(new { error = "Ya existe un equipo con ese nombre." });
        }

    }

    private static Equipo CrearEntidad(CrearEquipoRequest request)
    {
        return new Equipo { Nombre = request.Nombre };
    }

    [HttpDelete("borrar")]
    public IActionResult Borrar([FromBody] BorrarEquipoPorIdRequest request)
    {
        if (request == null)
            return BadRequest(new { error = "Los datos del equipo son obligatorios." });

        try
        {
            var filasEliminadas = _service.BorrarPorId(BorrarEntidad(request));
            return filasEliminadas == 0 ? NotFound() : Ok(filasEliminadas);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (MySqlException ex) when (ex.Number == 1451)
        {
            return Conflict(new { error = "No se puede eliminar el equipo porque tiene jugadores relacionados." });
        }
    }

    private static Equipo BorrarEntidad(BorrarEquipoPorIdRequest request)
    {
        return new Equipo
        {
            Id = request.Id
        };
    }
}
