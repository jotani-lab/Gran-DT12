using Dapper;
using System.Data;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoEquipo : IRepoEquipo
{
    private readonly IDbConnection _connection;
    public RepoEquipo(IDbConnection connection) { _connection = connection; }

    public List<Equipo> ObtenerTodos()
    {
        return _connection.Query<Equipo>("SELECT id AS Id, nombre AS Nombre FROM Equipo").ToList();
    }

    public Equipo? ObtenerPorId(int id)
    {
        return _connection.QueryFirstOrDefault<Equipo>("SELECT id AS Id, nombre AS Nombre FROM Equipo WHERE id = @id", new { id });
    }

    public int Crear(Equipo equipo)
    {
        string sql = "INSERT INTO Equipo (nombre) VALUES (@Nombre); SELECT LAST_INSERT_ID();";
        return _connection.ExecuteScalar<int>(sql, equipo);
    }

    public Equipo? ObtenerPorNombre(string nombre)
    {
        return _connection.QueryFirstOrDefault<Equipo>("SELECT id AS Id, nombre AS Nombre FROM Equipo WHERE nombre = @Nombre", new { nombre });
    }
    public int BorrarPorId(Equipo equipo)
    {
        const string sql = "DELETE FROM Equipo WHERE id = @Id";
        return _connection.Execute(sql, equipo);
    }
}
