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
    public bool Verificar(Equipo equipo)
        {        
            if (string.IsNullOrWhiteSpace(equipo.Nombre))
                return false;

            if (equipo.Nombre.Length > 100)
                return false;
            
            if (equipo.Nombre.Length <= 2)
                return false;

            return true;
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
        Verificar(equipo);
        return _repo.Crear(equipo);
    }
        
        
        
}