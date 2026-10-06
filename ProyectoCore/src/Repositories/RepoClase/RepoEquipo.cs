using Dapper;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoEquipo : IRepoEquipo
{
    private readonly DBConnection _db;
    public RepoEquipo(DBConnection db) { _db = db; }

    public List<Equipo> ObtenerTodos()
    {
        using var conn = _db.CreateConnection();
        return conn.Query<Equipo>("SELECT id AS Id, nombre AS Nombre FROM equipos").ToList();
    }

    public Equipo? ObtenerPorId(int id)
    {
        using var conn = _db.CreateConnection();
        return conn.QueryFirstOrDefault<Equipo>("SELECT id AS Id, nombre AS Nombre FROM equipos WHERE id = @id", new { id });
    }

    public int Crear(Equipo equipo)
    {
        using var conn = _db.CreateConnection();
        string sql = "INSERT INTO equipos (nombre) VALUES (@Nombre); SELECT LAST_INSERT_ID();";
        return conn.ExecuteScalar<int>(sql, equipo);
    }
}
