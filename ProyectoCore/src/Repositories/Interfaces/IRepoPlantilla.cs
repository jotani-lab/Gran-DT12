using ProyectoCore.Models;
namespace ProyectoCore.Repositories.Interfaces;
public interface IRepoPlantilla
{
    List<Plantilla> ObtenerPorUsuarioId(int usuarioId);
    Plantilla? ObtenerPorId(int id);
    int Crear(Plantilla plantilla);
    int BorrarPorId(Plantilla plantilla);
}
