using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServicePlantillaTitular
{
    private readonly IRepoPlantillaTitular _repo;
    public ServicePlantillaTitular(IRepoPlantillaTitular repo) { _repo = repo; }

    public bool AgregarTitular(int plantillaId, int jugadorId) => _repo.AgregarTitular(plantillaId, jugadorId);
    public bool RemoverTitular(int plantillaId, int jugadorId) => _repo.RemoverTitular(plantillaId, jugadorId);
}
