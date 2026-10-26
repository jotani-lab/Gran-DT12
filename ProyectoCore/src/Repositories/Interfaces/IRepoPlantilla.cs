using ProyectoCore.Models;
namespace ProyectoCore.Repositories.Interfaces;
public interface IRepoPlantilla
{
    Plantilla? ObtenerPorUsuarioId(int usuarioId);
    Plantilla? ObtenerPorId(int id);
    int Crear(Plantilla plantilla);
}
