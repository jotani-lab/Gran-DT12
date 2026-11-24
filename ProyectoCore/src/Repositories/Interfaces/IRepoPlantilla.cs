using ProyectoCore.Models;
namespace ProyectoCore.Repositories.Interfaces;
public interface IRepoPlantilla
{
    List<Plantilla> ObtenerPorUsuarioId(int usuarioId);
    Plantilla? ObtenerPorId(int id);
    decimal PuntajeTitularesFecha(int plantillaId, int nroMes, int nroFecha);
    int Crear(Plantilla plantilla);
    int BorrarPorId(Plantilla plantilla);
}
