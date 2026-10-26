using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServiceUsuario
{
    private readonly IRepoUsuario _repo;
    public ServiceUsuario(IRepoUsuario repo) { _repo = repo; }

    public Usuario? Login(string email, string password)
    {
        var usuario = _repo.ObtenerPorEmail(email);
        if (usuario == null) return null;
        return usuario.ValidarPassword(password) ? usuario : null;
    }

    public int Registrar(Usuario usuario, string rawPassword)
    {
        var existente = _repo.ObtenerPorEmail(usuario.Email);
        if (existente != null)
            throw new InvalidOperationException("El email ya se encuentra registrado.");

        usuario.EstablecerPassword(rawPassword);
        return _repo.Crear(usuario);
    }

    public Usuario? ObtenerPorId(int id) => _repo.ObtenerPorId(id);
}
