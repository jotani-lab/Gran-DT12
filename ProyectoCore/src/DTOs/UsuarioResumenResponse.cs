using ProyectoCore.Models;

namespace ProyectoCore.DTOs;

public class UsuarioResumenResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;

    public static UsuarioResumenResponse DesdeUsuario(Usuario usuario) => new()
    {
        Id = usuario.Id,
        Nombre = usuario.Nombre,
        Apellido = usuario.Apellido
    };
}
