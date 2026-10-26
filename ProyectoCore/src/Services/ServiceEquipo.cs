using System;
using System.Collections.Generic;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServiceEquipo
{
    private readonly IRepoEquipo _repo;

    public ServiceEquipo(IRepoEquipo repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public List<Equipo> ObtenerTodos() => _repo.ObtenerTodos();

    public Equipo? ObtenerPorId(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "El id del equipo debe ser mayor que cero.");

        return _repo.ObtenerPorId(id);
    }

    public int Crear(Equipo equipo)
    {
        ValidarEquipo(equipo);
        return _repo.Crear(equipo);
    }

    private static void ValidarEquipo(Equipo equipo)
    {
        if (equipo is null)
            throw new ArgumentNullException(nameof(equipo));

        if (string.IsNullOrWhiteSpace(equipo.Nombre))
            throw new ArgumentException("El nombre del equipo es obligatorio.", nameof(equipo));

        if (equipo.Nombre.Trim().Length < 2)
            throw new ArgumentException("El nombre del equipo debe tener al menos 2 caracteres.", nameof(equipo));
    }
}