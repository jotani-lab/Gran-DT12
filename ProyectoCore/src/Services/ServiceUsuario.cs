using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ProyectoCore.Services;

public class ServiceUsuario
{
    private readonly IRepoUsuario _repo;
    public ServiceUsuario(IRepoUsuario repo) { _repo = repo; }

    public List<string> Validar(Usuario usuario, string rawPassword)
    {
        var errores = new List<string>();

        if (usuario is null)
        {
            errores.Add("Se debe ingresar un usuario.");
            return errores;
        }

        if (string.IsNullOrWhiteSpace(usuario.Nombre))
            errores.Add("El nombre es obligatorio.");
        else if (usuario.Nombre.Length > 100)
            errores.Add("El nombre no puede superar los 100 caracteres.");

        if (string.IsNullOrWhiteSpace(usuario.Apellido))
            errores.Add("El apellido es obligatorio.");
        else if (usuario.Apellido.Length > 100)
            errores.Add("El apellido no puede superar los 100 caracteres.");

        if (usuario.FechaNacimiento == default || usuario.FechaNacimiento == DateTime.MinValue)
            errores.Add("La fecha de nacimiento no es válida.");

        if (string.IsNullOrWhiteSpace(usuario.Email))
            errores.Add("El email es obligatorio.");
        else if (usuario.Email.Length > 150)
            errores.Add("El email no puede superar los 150 caracteres.");

        if (string.IsNullOrWhiteSpace(rawPassword))
            errores.Add("La contraseña es obligatoria.");

        return errores;
    }

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

    public int BorrarPorId(Usuario usuario)
    {
        if (usuario is null)
            throw new ArgumentException("El usuario no existe.");

        if (usuario.Id <= 0)
            throw new ArgumentException("ID no válido.");

        return _repo.BorrarPorId(usuario);
    }

    public Usuario? ObtenerPorId(int id) => _repo.ObtenerPorId(id);
}
