using ProyectoCore.Models;
namespace ProyectoCore.Repositories.Interfaces;
public interface IRepoPlantillaSuplente
{
    bool AgregarSuplente(int plantillaId, int jugadorId);
    bool RemoverSuplente(int plantillaId, int jugadorId);
}
