using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ProyectoCore.Services;

public class ServiceUsuario
{
    private readonly IRepoUsuario _repo;
    public ServiceUsuario(IRepoUsuario repo) { _repo = repo; }


    public string EstablecerPassword(string rawPassword)
    {
        if (string.IsNullOrWhiteSpace(rawPassword))
            throw new ArgumentException("La contraseña no puede estar vacía.");

        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
        StringBuilder builder = new StringBuilder(64);
        foreach (byte b in bytes) builder.Append(b.ToString("x2"));
        return builder.ToString();
    }
    public bool ValidarPassword(string rawPassword, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(rawPassword)) return false;
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
        StringBuilder builder = new StringBuilder(64);
        foreach (byte b in bytes) builder.Append(b.ToString("x2"));
        return passwordHash.Equals(builder.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public bool Verificar(string nombre, string apellido, string email, DateTime fechaNacimiento, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return false;
        if (nombre.Length > 100)
            return false;

        if (string.IsNullOrWhiteSpace(apellido))
            return false;
        if (apellido.Length > 100)
            return false;

        if (fechaNacimiento == default || fechaNacimiento == DateTime.MinValue)
            return false;

        if (string.IsNullOrWhiteSpace(email))
            return false;
        if (email.Length > 150)
            return false;

        if (string.IsNullOrWhiteSpace(passwordHash))
            return false;
        if (passwordHash.Length != 64)
            return false;

        return true;
    }

    public Usuario? Login(string email, string password)
    {
        var usuario = _repo.ObtenerPorEmail(email);
        if (usuario == null) return null;
        return ValidarPassword(password, usuario.PasswordHash) ? usuario : null;
    }

    public int Registrar(Usuario usuario, string rawPassword)
    {
        var existente = _repo.ObtenerPorEmail(usuario.Email);
        if (existente != null)
            throw new InvalidOperationException("El email ya se encuentra registrado.");

        string passwordHash = EstablecerPassword(rawPassword);
        usuario.PasswordHash = passwordHash;
        return _repo.Crear(usuario);
    }

    public Usuario? ObtenerPorId(int id) => _repo.ObtenerPorId(id);
}
