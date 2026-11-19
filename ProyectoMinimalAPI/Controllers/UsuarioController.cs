using Microsoft.AspNetCore.Mvc;
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
        var user = _service.Login(req.Email, req.Password);
        if (user == null) return Unauthorized(new { error = "Credenciales inválidas." });
        return Ok(user);
    }

    [HttpPost("registro")]
    public IActionResult Registro([FromBody] RegistroRequest req)
    {
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
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("borrar")]
    public IActionResult Borrar([FromBody] BorrarUsuarioPorIdRequest request)
    {
        try
        {
            if (request == null)
                return BadRequest(new { error = "Los datos del usuario son obligatorios." });

            var usuario = CrearEntidad(request);
            var filasEliminadas = _service.BorrarPorId(usuario);
            return filasEliminadas == 0 ? NotFound() : Ok(filasEliminadas);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
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
