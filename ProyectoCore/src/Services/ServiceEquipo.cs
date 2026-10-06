using System;
using System.Collections.Generic;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServiceEquipo
{
    private readonly IRepoEquipo _repo;
    public ServiceEquipo(IRepoEquipo repo) { _repo = repo; }

    public List<Equipo> ObtenerTodos() => _repo.ObtenerTodos();
    public Equipo? ObtenerPorId(int id) => _repo.ObtenerPorId(id);

    public int Crear(Equipo equipo)
    {
        if (string.IsNullOrWhiteSpace(equipo.Nombre))
            throw new ArgumentException("El nombre del equipo es obligatorio.");

        return _repo.Crear(equipo);
    }
}