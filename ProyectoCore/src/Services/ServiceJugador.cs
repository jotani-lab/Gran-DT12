using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServiceJugador
{
    private readonly IRepoJugador _repo;
    public ServiceJugador(IRepoJugador repo) { _repo = repo; }

    public List<string> Validar(Jugador jugador)
    {
        var errores = new List<string>();

        if (jugador is null)
        {
            errores.Add("Se debe ingresar un jugador.");
            return errores;
        }

        if (string.IsNullOrWhiteSpace(jugador.Nombre))
            errores.Add("El nombre es obligatorio.");
        else if (jugador.Nombre.Length > 100)
            errores.Add("El nombre no puede superar los 100 caracteres.");

        if (string.IsNullOrWhiteSpace(jugador.Apellido))
            errores.Add("El apellido es obligatorio.");
        else if (jugador.Apellido.Length > 100)
            errores.Add("El apellido no puede superar los 100 caracteres.");

        if (!string.IsNullOrEmpty(jugador.Apodo) && jugador.Apodo.Length > 100)
            errores.Add("El apodo no puede superar los 100 caracteres.");

        if (jugador.EquipoId <= 0)
            errores.Add("El equipo no es válido.");

        if (jugador.FechaNacimiento == default ||
            jugador.FechaNacimiento == DateTime.MinValue)
            errores.Add("La fecha de nacimiento no es válida.");

        if (jugador.PosicionId <= 0 || jugador.PosicionId > 4)
            errores.Add("La posición no es válida.");

        if (jugador.Cotizacion < 0 || jugador.Cotizacion > 99999999.99m)
            errores.Add("La cotización debe estar entre $0 y $99.999.999,99.");

        return errores;
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

    public int BorrarPorId(Jugador jugador)
    {
        if (jugador is null)
            throw new ArgumentException("El jugador no existe.");

        if (jugador.Id <= 0)
            throw new ArgumentException("ID no válido.");

        return _repo.BorrarPorId(jugador);
    }
    
}
