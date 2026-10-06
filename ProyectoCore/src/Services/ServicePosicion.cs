using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServicePosicion
{
    private readonly IRepoPosicion _repo;
    public ServicePosicion(IRepoPosicion repo) { _repo = repo; }

    public List<Posicion> ObtenerTodas() => _repo.ObtenerTodas();
    public Posicion? ObtenerPorId(int id) => _repo.ObtenerPorId(id);
}