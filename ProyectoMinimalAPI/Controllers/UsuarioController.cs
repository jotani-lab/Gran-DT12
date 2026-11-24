using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ProyectoCore.DTOs;
using ProyectoCore.Models;
using ProyectoCore.Services;

namespace ProyectoMinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ServiceUsuario _service;
    public UsuarioController(ServiceUsuario service) { _service = service; }

    [HttpPost("verificar")]
    public IActionResult Verificar([FromBody] RegistroRequest req)
    {
        if (req == null)
            return BadRequest(new { errores = new[] { "Los datos del usuario son obligatorios." } });

        var usuario = new Usuario
        {
            Nombre = req.Nombre,
            Apellido = req.Apellido,
            Email = req.Email,
            FechaNacimiento = req.FechaNacimiento
        };

        return Ok(_service.Validar(usuario, req.Password));
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        if (req == null || string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Password))
            return BadRequest(new { error = "Email y contraseña son obligatorios." });

        var user = _service.Login(req.Email, req.Password);
        if (user == null) return Unauthorized(new { error = "Credenciales inválidas." });
        return Ok(user);
    }

    [HttpPost("registro")]
    public IActionResult Registro([FromBody] RegistroRequest req)
    {
        if (req == null)
            return BadRequest(new { error = "Los datos del usuario son obligatorios." });

        try
        {
            var usuario = new Usuario
            {
                Nombre = req.Nombre,
                Apellido = req.Apellido,
                Email = req.Email,
                FechaNacimiento = req.FechaNacimiento
            };

            var errores = _service.Validar(usuario, req.Password);
            if (errores.Count > 0)
                return BadRequest(new { errores });

            int id = _service.Registrar(usuario, req.Password);
            return Ok(new { id, message = "Usuario registrado correctamente." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (MySqlException ex) when (ex.Number == 1062)
        {
            return Conflict(new { error = "Ya existe un usuario registrado con ese email." });
        }
    }

    [HttpDelete("borrar")]
    public IActionResult Borrar([FromBody] BorrarUsuarioPorIdRequest request)
    {
        if (request == null)
            return BadRequest(new { error = "Los datos del usuario son obligatorios." });

        try
        {
            var usuario = CrearEntidad(request);
            var noEncontrado = _service.BorrarPorId(usuario);
            return noEncontrado
                ? NotFound(new { error = "No existe el usuario indicado." })
                : Ok(new { message = "Usuario eliminado correctamente." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (MySqlException ex) when (ex.Number == 1451)
        {
            return Conflict(new { error = "No se puede eliminar el usuario porque tiene datos relacionados." });
        }
    }

    private static Usuario CrearEntidad(BorrarUsuarioPorIdRequest request)
    {
        return new Usuario
        {
            Id = request.Id
        };
    }
}
