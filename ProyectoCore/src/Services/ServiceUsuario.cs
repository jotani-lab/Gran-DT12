using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;
using ProyectoCore.Security;

namespace ProyectoCore.Services;

public class ServiceUsuario
{
    private readonly IRepoUsuario _repo;
    private readonly IEncriptador _encriptador;

    public ServiceUsuario(IRepoUsuario repo, IEncriptador encriptador)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _encriptador = encriptador ?? throw new ArgumentNullException(nameof(encriptador));
    }

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

        if (usuario.FechaNacimiento == default || usuario.FechaNacimiento == DateOnly.MinValue)
            errores.Add("La fecha de nacimiento no es válida.");

        if (string.IsNullOrWhiteSpace(usuario.Email))
            errores.Add("El email es obligatorio.");
        else if (usuario.Email.Length > 150)
            errores.Add("El email no puede superar los 150 caracteres.");

        if (string.IsNullOrWhiteSpace(rawPassword))
            errores.Add("La contraseña es obligatoria.");

        return errores;
    }


    public Usuario? Login(string email, string password)
    {
        var usuario = _repo.ObtenerPorEmail(email);
        if (usuario == null) return null;
        return _encriptador.Validar(password, usuario.PasswordHash) ? usuario : null;
    }

    public int Registrar(Usuario usuario, string rawPassword)
    {
        var errores = Validar(usuario, rawPassword);
        if (errores.Count > 0)
            throw new ArgumentException(string.Join(" ", errores), nameof(usuario));

        var existente = _repo.ObtenerPorEmail(usuario.Email);
        if (existente != null)
            throw new InvalidOperationException("El email ya se encuentra registrado.");

        usuario.PasswordHash = _encriptador.Encriptar(rawPassword);
        return _repo.Crear(usuario);
    }

    public bool BorrarPorId(Usuario usuario)
    {
        if (usuario is null)
            throw new ArgumentException("El usuario no existe.");

        if (usuario.Id <= 0)
            throw new ArgumentException("ID no válido.");

        return _repo.BorrarPorId(usuario) > 0;
    }

    public Usuario? ObtenerPorId(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "El ID del usuario debe ser mayor a cero.");
        return _repo.ObtenerPorId(id);
    }

    public List<Usuario> ObtenerTodos() => _repo.ObtenerTodos();
}
