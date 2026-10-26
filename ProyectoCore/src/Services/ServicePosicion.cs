using System;
using System.Collections.Generic;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServicePosicion
{
    private readonly IRepoPosicion _repo;

    public ServicePosicion(IRepoPosicion repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public List<Posicion> ObtenerTodas() => _repo.ObtenerTodas();

    public Posicion? ObtenerPorId(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "El id de la posición debe ser mayor que cero.");

        return _repo.ObtenerPorId(id);
    }

    public void Crear(Posicion posicion)
    {
        if (posicion is null)
            throw new ArgumentNullException(nameof(posicion));

        if (!posicion.Verificar())
            throw new ArgumentException("La posición no es válida.", nameof(posicion));

        // La interfaz actual no expone un método de creación.
        // Si luego agregas Create/Insert en IRepoPosicion, se implementa aquí.
    }
}