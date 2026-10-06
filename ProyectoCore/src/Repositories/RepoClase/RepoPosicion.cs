using Dapper;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoPosicion : IRepoPosicion
{
    private readonly DBConnection _db;
    public RepoPosicion(DBConnection db) { _db = db; }

    public List<Posicion> ObtenerTodas()
    {
        using var conn = _db.CreateConnection();
        return conn.Query<Posicion>("SELECT id AS Id, nombre AS Nombre FROM posiciones").ToList();
    }

    public Posicion? ObtenerPorId(int id)
    {
        using var conn = _db.CreateConnection();
        return conn.QueryFirstOrDefault<Posicion>("SELECT id AS Id, nombre AS Nombre FROM posiciones WHERE id = @id", new { id });
    }
}
