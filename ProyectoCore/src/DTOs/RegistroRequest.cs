namespace ProyectoCore.DTOs;

public class RegistroRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; }
    public string Password { get; set; } = string.Empty;
}
