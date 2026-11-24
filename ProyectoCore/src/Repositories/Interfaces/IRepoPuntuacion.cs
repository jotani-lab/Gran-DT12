using ProyectoCore.Models;

namespace ProyectoCore.Repositories.Interfaces;

public interface IRepoPuntuacion
{
    bool RegistrarPuntuacion(Puntuacion puntuacion);
    bool ActualizarPuntuacion(Puntuacion puntuacion);
    bool BorrarPorId(int idPuntuacion);
    Puntuacion? ObtenerPorId(int idPuntuacion);
    List<Puntuacion> ObtenerTodas();
    List<Puntuacion> ObtenerPorJugadorId(int jugadorId);
    List<Puntuacion> ObtenerPorMes(int nroMes);
    Puntuacion? ObtenerPorJugadorMesFecha(int jugadorId, int nroMes, int nroFecha);
}
