using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServicePlantillaSuplente
{
    private readonly IRepoPlantillaSuplente _repo;
    public ServicePlantillaSuplente(IRepoPlantillaSuplente repo) { _repo = repo; }

    //to do : agregar validaciones de existencia de plantilla y jugador antes de agregar o remover titulares
    public bool AgregarSuplente(int plantillaId, int jugadorId) => _repo.AgregarSuplente(plantillaId, jugadorId);
    public bool RemoverSuplente(int plantillaId, int jugadorId) => _repo.RemoverSuplente(plantillaId, jugadorId);
}
