using ProyectoCore.Models;
namespace ProyectoCore.Repositories.Interfaces;
public interface IRepoJugador
{
    List<Jugador> ObtenerTodos();
    Jugador? ObtenerPorId(int id);
    int Crear(Jugador jugador);
}
