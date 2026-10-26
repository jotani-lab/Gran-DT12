using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServicePuntuacion
{
    private readonly IRepoPuntuacion _repo;

    public ServicePuntuacion(IRepoPuntuacion repo)
    {
        _repo = repo;
    }

    public bool RegistrarPuntuacion(Puntuacion puntuacion)
    {
        if (puntuacion is null)
            throw new ArgumentNullException(nameof(puntuacion));

        if (!puntuacion.Verificar())
            throw new ArgumentException("La puntuación no es válida.", nameof(puntuacion));

        return _repo.RegistrarPuntuacion(puntuacion);
    }

    public List<Puntuacion> ObtenerPorJugadorId(int jugadorId)
    {
        if (jugadorId <= 0)
            throw new ArgumentOutOfRangeException(nameof(jugadorId), "El id del jugador debe ser mayor que cero.");

        return _repo.ObtenerPorJugadorId(jugadorId);
    }
}
