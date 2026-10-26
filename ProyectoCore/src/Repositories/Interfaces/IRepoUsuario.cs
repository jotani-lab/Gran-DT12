using ProyectoCore.Models;
namespace ProyectoCore.Repositories.Interfaces;
public interface IRepoUsuario
{
    List<Usuario> ObtenerTodos();
    Usuario? ObtenerPorEmail(string email);
    Usuario? ObtenerPorId(int id);
    int Crear(Usuario usuario);
}
