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

    public List<string> Validar(Puntuacion puntuacion)
    {
        var errores = new List<string>();

        if (puntuacion is null)
        {
            errores.Add("Se debe ingresar una puntuación.");
            return errores;
        }

        if (puntuacion.JugadorId <= 0)
            errores.Add("El jugador no es válido.");

        if (puntuacion.NroFecha <= 0 || puntuacion.NroFecha >= 50)
            errores.Add("El número de fecha debe estar entre 1 y 49.");

        if (puntuacion.Nota < 1.0m || puntuacion.Nota > 10.0m)
            errores.Add("La nota debe estar entre 1.0 y 10.0.");

        return errores;
    }

    public bool RegistrarPuntuacion(Puntuacion puntuacion)
    {
        var errores = Validar(puntuacion);
        if (errores.Count > 0)
            throw new ArgumentException(string.Join(" ", errores), nameof(puntuacion));

        return _repo.RegistrarPuntuacion(puntuacion);
    }

    public List<Puntuacion> ObtenerPorJugadorId(int jugadorId)
    {
        if (jugadorId <= 0)
            throw new ArgumentOutOfRangeException(nameof(jugadorId), "El id del jugador debe ser mayor que cero.");

        return _repo.ObtenerPorJugadorId(jugadorId);
    }
}
