using Dapper;
using System.Data;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoPosicion : IRepoPosicion
{
    private readonly IDbConnection _connection;
    public RepoPosicion(IDbConnection connection) { _connection = connection; }

    public List<Posicion> ObtenerTodas()
    {
        return _connection.Query<Posicion>("SELECT id AS Id, nombre AS Nombre FROM Posicion").ToList();
    }

    public Posicion? ObtenerPorId(int id)
    {
        return _connection.QueryFirstOrDefault<Posicion>("SELECT id AS Id, nombre AS Nombre FROM Posicion WHERE id = @id", new { id });
    }
}
