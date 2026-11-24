using Dapper;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoPlantilla : IRepoPlantilla
{
    private readonly DBConnection _db;
    public RepoPlantilla(DBConnection db) { _db = db; }

    public List<Plantilla> ObtenerPorUsuarioId(int usuarioId)
    {
        using var conn = _db.CreateConnection();
        string sql = "SELECT id AS Id, usuarioId AS UsuarioId, nombre AS Nombre, presupuestoMaximo AS Presupuesto, cantidadMaximaJugadores AS CantidadJugadores FROM Plantilla WHERE usuarioId = @usuarioId";
        return conn.Query<Plantilla>(sql, new { usuarioId }).ToList();
    }

    public Plantilla? ObtenerPorId(int id)
    {
        using var conn = _db.CreateConnection();
        string sql = "SELECT id AS Id, usuarioId AS UsuarioId, nombre AS Nombre, presupuestoMaximo AS Presupuesto, cantidadMaximaJugadores AS CantidadJugadores FROM Plantilla WHERE id = @id";
        return conn.QueryFirstOrDefault<Plantilla>(sql, new { id });
    }

    public int Crear(Plantilla plantilla)
    {
        using var conn = _db.CreateConnection();
        string sql = @"INSERT INTO Plantilla (usuarioId, nombre, presupuestoMaximo, cantidadMaximaJugadores)
                       VALUES (@UsuarioId, @Nombre, @Presupuesto, @CantidadJugadores);
                       SELECT LAST_INSERT_ID();";
        return conn.ExecuteScalar<int>(sql, plantilla);
    }

    public int BorrarPorId(Plantilla plantilla)
    {
        using var conn = _db.CreateConnection();
        string sql = "DELETE FROM Plantilla WHERE id = @Id";
        return conn.Execute(sql, plantilla);
    }
}
