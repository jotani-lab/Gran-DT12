using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServiceJugador
{
    private readonly IRepoJugador _repo;
    public ServiceJugador(IRepoJugador repo) { _repo = repo; }

    //TO DO implementar esto en el controller de jugador
    public bool Verificar(string nombre, string apellido, string apodo, int equipoId, DateTime fechaNacimiento, int posicionId, decimal cotizacion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return false;
        if (nombre.Length > 100)
            return false;

        if (string.IsNullOrWhiteSpace(apellido))
            return false;
        if (apellido.Length > 100)
            return false;

        if (apodo.Length > 100)
            return false;

        if (fechaNacimiento == default || fechaNacimiento == DateTime.MinValue)
        return false;

        if (equipoId <= 0)
            return false;

        if (posicionId <= 0)
            return false;
        if (posicionId > 4)
            return false;

        if (cotizacion < 0 || cotizacion > 99999999.99m)
            return false;

        return true;
    }

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
