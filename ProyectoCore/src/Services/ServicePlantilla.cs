using System;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServicePlantilla
{
    private readonly IRepoPlantilla _repo;
    public ServicePlantilla(IRepoPlantilla repo) { _repo = repo; }

    public Plantilla? ObtenerPorUsuarioId(int usuarioId) => _repo.ObtenerPorUsuarioId(usuarioId);
    public Plantilla? ObtenerPorId(int id) => _repo.ObtenerPorId(id);

    public int Crear(Plantilla plantilla)
    {
        if (string.IsNullOrWhiteSpace(plantilla.Nombre))
            throw new ArgumentException("El nombre de la plantilla es obligatorio.");

        return _repo.Crear(plantilla);
    }
}