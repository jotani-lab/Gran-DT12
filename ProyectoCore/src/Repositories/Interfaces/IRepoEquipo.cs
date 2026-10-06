using ProyectoCore.Models;
namespace ProyectoCore.Repositories.Interfaces;
public interface IRepoEquipo
{
    List<Equipo> ObtenerTodos();
    Equipo? ObtenerPorId(int id);
    int Crear(Equipo equipo);
}
