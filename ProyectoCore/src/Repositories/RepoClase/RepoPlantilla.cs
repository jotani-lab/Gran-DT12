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
        string sql = "SELECT id AS Id, usuario_id AS UsuarioId, nombre AS Nombre, presupuesto_maximo AS Presupuesto, cantidad_maxima_jugadores AS CantidadJugadores FROM plantillas WHERE usuario_id = @usuarioId";
        return conn.Query<Plantilla>(sql, new { usuarioId }).ToList();
    }

    public Plantilla? ObtenerPorId(int id)
    {
        using var conn = _db.CreateConnection();
        string sql = "SELECT id AS Id, usuario_id AS UsuarioId, nombre AS Nombre, presupuesto_maximo AS Presupuesto, cantidad_maxima_jugadores AS CantidadJugadores FROM plantillas WHERE id = @id";
        return conn.QueryFirstOrDefault<Plantilla>(sql, new { id });
    }

    public int Crear(Plantilla plantilla)
    {
        using var conn = _db.CreateConnection();
        string sql = @"INSERT INTO plantillas (usuario_id, nombre, presupuesto_maximo, cantidad_maxima_jugadores) 
                       VALUES (@UsuarioId, @Nombre, @Presupuesto, @CantidadJugadores);
                       SELECT LAST_INSERT_ID();";
        return conn.ExecuteScalar<int>(sql, plantilla);
    }

    public int BorrarPorId(Plantilla plantilla)
    {
        using var conn = _db.CreateConnection();
        string sql = "DELETE FROM plantillas WHERE id = @Id";
        return conn.Execute(sql, plantilla);
    }
}
