using System.Security.Cryptography;
using System.Text;

namespace ProyectoCore.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public bool EsAdmin { get; set; } = false;
    public Plantilla? Plantilla { get; set; }
    
}
