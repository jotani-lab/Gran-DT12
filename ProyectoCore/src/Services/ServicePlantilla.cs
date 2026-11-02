using System;
using System.Linq;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServicePlantilla
{
    private readonly IRepoPlantilla _repo;
    public ServicePlantilla(IRepoPlantilla repo) { _repo = repo; }

    public bool Verificar(Plantilla plantilla)
    {
        if (plantilla is null)
            throw new ArgumentNullException(nameof(plantilla));

        if (plantilla.UsuarioId <= 0)
            return false;

        if (string.IsNullOrWhiteSpace(plantilla.Nombre))
            return false;

        if (plantilla.PresupuestoMaximo <= 0 || plantilla.PresupuestoMaximo > 100000000.00m)
            return false;

        if (plantilla.CantidadMaximaJugadores != 20)
            return false;

        return true;
    }

    public decimal PuntajeFecha(Plantilla plantilla, int nroFecha)
    {
        if (plantilla is null)
            throw new ArgumentNullException(nameof(plantilla));

        if (nroFecha <= 0 || nroFecha >= 50)
            throw new ArgumentOutOfRangeException(nameof(nroFecha), "Número de fecha inválido.");

        decimal total = 0m;
        foreach (var t in plantilla.Titulares)
        {
            if (t.Jugador != null)
            {
                var p = t.Jugador.Puntuaciones.FirstOrDefault(x => x.NroFecha == nroFecha);
                if (p != null) total += p.Nota;
            }
        }

        return total;
    }

    public Plantilla? ObtenerPorUsuarioId(int usuarioId) => _repo.ObtenerPorUsuarioId(usuarioId);
    public Plantilla? ObtenerPorId(int id) => _repo.ObtenerPorId(id);

    public int Crear(Plantilla plantilla)
    {
        if (plantilla is null)
            throw new ArgumentNullException(nameof(plantilla));

        if (!Verificar(plantilla))
            throw new ArgumentException("La plantilla no es válida.", nameof(plantilla));

        return _repo.Crear(plantilla);
    }
}