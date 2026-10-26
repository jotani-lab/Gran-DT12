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

    public void EstablecerPassword(string rawPassword)
    {
        if (string.IsNullOrWhiteSpace(rawPassword))
            throw new ArgumentException("La contraseña no puede estar vacía.");

        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
        StringBuilder builder = new StringBuilder(64);
        foreach (byte b in bytes) builder.Append(b.ToString("x2"));
        PasswordHash = builder.ToString();
    }

    public bool ValidarPassword(string rawPassword)
    {
        if (string.IsNullOrWhiteSpace(rawPassword)) return false;
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
        StringBuilder builder = new StringBuilder(64);
        foreach (byte b in bytes) builder.Append(b.ToString("x2"));
        return PasswordHash.Equals(builder.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public bool Verificar()
    {
        if (string.IsNullOrWhiteSpace(Nombre))
            return false;

        if (string.IsNullOrWhiteSpace(Apellido))
            return false;

        if (string.IsNullOrWhiteSpace(Email))
            return false;

        if (string.IsNullOrWhiteSpace(PasswordHash))
            return false;

        return true;
    }
}
