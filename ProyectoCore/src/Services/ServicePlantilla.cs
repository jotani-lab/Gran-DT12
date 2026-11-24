using System;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServicePlantilla
{
    private readonly IRepoPlantilla _repo;
    public ServicePlantilla(IRepoPlantilla repo) { _repo = repo; }

    public List<string> Validar(Plantilla plantilla)
    {
        var errores = new List<string>();

        if (plantilla is null)
        {
            errores.Add("Se debe ingresar una plantilla.");
            return errores;
        }

        if (plantilla.UsuarioId <= 0)
            errores.Add("El usuario no es válido.");

        if (string.IsNullOrWhiteSpace(plantilla.Nombre))
            errores.Add("El nombre de la plantilla es obligatorio.");

        if (plantilla.Presupuesto <= 0)
            errores.Add("El presupuesto debe ser mayor que cero.");
        else if (plantilla.Presupuesto > 100000000.00m)
            errores.Add("El presupuesto no puede superar $100.000.000.");

        if (plantilla.CantidadJugadores != 20)
            errores.Add("La cantidad máxima de jugadores debe ser 20.");

        return errores;
    }

    public decimal PuntajeFecha(Plantilla plantilla, int nroMes, int nroFecha)
    {
        if (plantilla is null)
            throw new ArgumentNullException(nameof(plantilla));
        if (plantilla.Id <= 0)
            throw new ArgumentOutOfRangeException(nameof(plantilla.Id), "El ID de la plantilla debe ser mayor a cero.");
        if (nroMes <= 0 || nroMes > 12)
            throw new ArgumentOutOfRangeException(nameof(nroMes), "El número de mes debe estar entre 1 y 12.");

        if (nroFecha <= 0 || nroFecha >= 50)
            throw new ArgumentOutOfRangeException(nameof(nroFecha), "Número de fecha inválido.");

        return _repo.PuntajeTitularesFecha(plantilla.Id, nroMes, nroFecha);
    }

    public Plantilla? ObtenerPorId(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "El ID de la plantilla debe ser mayor a cero.");
        return _repo.ObtenerPorId(id);
    }

    public List<Plantilla> ObtenerPorUsuarioId(int usuarioId)
    {
        if (usuarioId <= 0)
            throw new ArgumentOutOfRangeException(nameof(usuarioId), "El ID del usuario debe ser mayor a cero.");
        return _repo.ObtenerPorUsuarioId(usuarioId);
    }

    public int Crear(Plantilla plantilla)
    {
        var errores = Validar(plantilla);
        if (errores.Count > 0)
            throw new ArgumentException(string.Join(" ", errores), nameof(plantilla));

        return _repo.Crear(plantilla);
    }

    public int BorrarPorId(Plantilla plantilla)
    {
        if (plantilla is null)
            throw new ArgumentException("La plantilla no existe.");

        if (plantilla.Id <= 0)
            throw new ArgumentException("ID no válido.");

        return _repo.BorrarPorId(plantilla);
    }
}