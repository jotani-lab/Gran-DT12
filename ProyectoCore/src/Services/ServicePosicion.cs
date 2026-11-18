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

    public List<string> Validar(Posicion posicion)
    {
        var errores = new List<string>();

        if (posicion is null)
        {
            errores.Add("Se debe ingresar una posición.");
            return errores;
        }

        if (posicion.Id <= 0)
            errores.Add("El id de la posición debe ser mayor que cero.");
        else if (posicion.Id >= 5)
            errores.Add("El id de la posición debe ser menor que 5.");

        if (string.IsNullOrWhiteSpace(posicion.Nombre))
            errores.Add("El nombre de la posición es obligatorio.");

        return errores;
    }

    public List<Posicion> ObtenerTodas() => _repo.ObtenerTodas();

    public Posicion? ObtenerPorId(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "El id de la posición debe ser mayor que cero.");
        if (id >= 5)
            throw new ArgumentOutOfRangeException(nameof(id), "El id de la posición debe ser menor a 5.");
        return _repo.ObtenerPorId(id);
    }
}