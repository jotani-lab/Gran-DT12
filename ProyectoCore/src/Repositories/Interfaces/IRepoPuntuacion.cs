using ProyectoCore.Models;
namespace ProyectoCore.Repositories.Interfaces;
public interface IRepoPuntuacion
{
    bool RegistrarPuntuacion(Puntuacion puntuacion);
    List<Puntuacion> ObtenerPorJugadorId(int jugadorId);
}
