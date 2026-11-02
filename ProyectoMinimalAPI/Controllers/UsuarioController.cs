using Microsoft.AspNetCore.Mvc;
using ProyectoCore.Models;
using ProyectoCore.Services;

namespace ProyectoMinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ServiceUsuario _service;
    public UsuarioController(ServiceUsuario service) { _service = service; }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegistroRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Password { get; set; } = string.Empty;
    }

    [HttpPost("verificar")]
    public IActionResult Verificar([FromBody] RegistroRequest req)
    {
        if (req == null)
            return BadRequest(new { error = "Los datos del usuario son obligatorios." });

        string passwordHash = _service.EstablecerPassword(req.Password);
        return Ok(_service.Verificar(req.Nombre, req.Apellido, req.Email, req.FechaNacimiento, passwordHash));
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
            var u = new Usuario
            {
                Nombre = req.Nombre,
                Apellido = req.Apellido,
                Email = req.Email,
                FechaNacimiento = req.FechaNacimiento
            };

            string passwordHash = _service.EstablecerPassword(req.Password);
            if (!_service.Verificar(req.Nombre, req.Apellido, req.Email, req.FechaNacimiento, passwordHash))
                return BadRequest(new { error = "Los datos del usuario no son válidos." });

            int id = _service.Registrar(u, req.Password);
            return Ok(new { id, message = "Usuario registrado correctamente." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
