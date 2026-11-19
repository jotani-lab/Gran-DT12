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

    public List<string> Validar(Equipo equipo)
    {
        var errores = new List<string>();

        if (equipo is null)
        {
            errores.Add("Se debe ingresar un equipo.");
            return errores;
        }

        if (string.IsNullOrWhiteSpace(equipo.Nombre))
            errores.Add("El nombre del equipo es obligatorio.");
        else
        {
            if (equipo.Nombre.Length <= 2)
                errores.Add("El nombre del equipo debe tener más de 2 caracteres.");

            if (equipo.Nombre.Length > 100)
                errores.Add("El nombre del equipo no puede superar los 100 caracteres.");
        }

        return errores;
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
        var errores = Validar(equipo);
        if (errores.Count > 0)
            throw new ArgumentException(string.Join(" ", errores), nameof(equipo));

        return _repo.Crear(equipo);
    }

    public int BorrarPorId(Equipo equipo)
    {
        if (equipo is null)
            throw new ArgumentException("El equipo no existe.");

        if (equipo.Id <= 0)
            throw new ArgumentException("ID no válido.");

        return _repo.BorrarPorId(equipo);
    }
}