using ProyectoCore.Models;
namespace ProyectoCore.Repositories.Interfaces;
public interface IRepoPlantillaTitular
{
    bool AgregarTitular(int plantillaId, int jugadorId);
    bool RemoverTitular(int plantillaId, int jugadorId);
}
