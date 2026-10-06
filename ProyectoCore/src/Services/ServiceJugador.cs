using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServiceJugador
{
    private readonly IRepoJugador _repo;
    public ServiceJugador(IRepoJugador repo) { _repo = repo; }

    public List<Jugador> ObtenerTodos() => _repo.ObtenerTodos();
    public Jugador? ObtenerPorId(int id) => _repo.ObtenerPorId(id);

    public int Crear(Jugador jugador)
    {
        if (string.IsNullOrWhiteSpace(jugador.Nombre) || string.IsNullOrWhiteSpace(jugador.Apellido))
            throw new ArgumentException("El nombre y apellido del jugador son obligatorios.");

        if (jugador.Cotizacion < 0 || jugador.Cotizacion > 99999999.99m)
            throw new ArgumentOutOfRangeException(nameof(jugador.Cotizacion), "La cotización debe estar entre $0 y $99.999.999,99.");

        return _repo.Crear(jugador);
    }
}
