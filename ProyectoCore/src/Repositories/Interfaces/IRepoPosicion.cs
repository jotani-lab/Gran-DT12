using ProyectoCore.Models;
namespace ProyectoCore.Repositories.Interfaces;
public interface IRepoPosicion
{
    List<Posicion> ObtenerTodas();
    Posicion? ObtenerPorId(int id);
}
