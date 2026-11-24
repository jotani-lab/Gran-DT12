using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ProyectoCore.DTOs;
using ProyectoCore.Models;
using ProyectoCore.Services;

namespace ProyectoMinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaController : ControllerBase
{
    private readonly ServicePlantilla _service;
    public PlantillaController(ServicePlantilla service) { _service = service; }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var res = _service.ObtenerPorId(id);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpGet("usuario/{usuarioId}")]
    public IActionResult GetByUsuario(int usuarioId)
    {
        var res = _service.ObtenerPorUsuarioId(usuarioId);
        return Ok(res);
    }

    [HttpGet("{id}/puntaje-fecha/{nroFecha}")]
    public IActionResult PuntajeFecha(int id, int nroFecha)
    {
        var plantilla = _service.ObtenerPorId(id);
        if (plantilla == null)
            return NotFound();

        try { return Ok(_service.PuntajeFecha(plantilla, nroFecha)); }
        catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
    }

    [HttpPost("verificar")]
    public IActionResult Verificar([FromBody] CrearPlantillaRequest request)
    {
        var plantilla = CrearEntidad(request);
        return Ok(_service.Validar(plantilla));
    }

    [HttpPost]
    public IActionResult Create([FromBody] CrearPlantillaRequest request)
    {
        try
        {
            var plantilla = CrearEntidad(request);
            var errores = _service.Validar(plantilla);
            if (errores.Count > 0)
                return BadRequest(new { errores });

            int id = _service.Crear(plantilla);
            return Ok(new { id, message = "Plantilla creada correctamente." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (MySqlException ex) when (ex.Number == 1452)
        {
            return BadRequest(new { error = "No existe el usuario indicado." });
        }
        catch (MySqlException ex) when (ex.Number == 1062)
        {
            return Conflict(new { error = "El usuario ya tiene una plantilla registrada." });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Borrar(int id)
    {
        try
        {
            var plantilla = new Plantilla { Id = id };
            var filasEliminadas = _service.BorrarPorId(plantilla);
            return filasEliminadas == 0 ? NotFound() : Ok(filasEliminadas);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (MySqlException ex) when (ex.Number == 1451)
        {
            return Conflict(new { error = "No se puede eliminar la plantilla por sus datos relacionados." });
        }
    }

    private static Plantilla CrearEntidad(CrearPlantillaRequest request)
    {
        return new Plantilla
        {
            UsuarioId = request.UsuarioId,
            Nombre = string.IsNullOrWhiteSpace(request.Nombre)
                ? "Mi Plantilla"
                : request.Nombre,
            Presupuesto = 100000000.00m,
            CantidadJugadores = 20
        };
    }
}
