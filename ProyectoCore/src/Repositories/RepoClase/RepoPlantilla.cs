using Dapper;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoPlantilla : IRepoPlantilla
{
    private readonly DBConnection _db;
    public RepoPlantilla(DBConnection db) { _db = db; }

    public Plantilla? ObtenerPorUsuarioId(int usuarioId)
    {
        using var conn = _db.CreateConnection();
        string sql = "SELECT id AS Id, usuario_id AS UsuarioId, nombre AS Nombre, presupuesto_maximo AS PresupuestoMaximo, cantidad_maxima_jugadores AS CantidadMaximaJugadores FROM plantillas WHERE usuario_id = @usuarioId";
        return conn.QueryFirstOrDefault<Plantilla>(sql, new { usuarioId });
    }

    public Plantilla? ObtenerPorId(int id)
    {
        using var conn = _db.CreateConnection();
        string sql = "SELECT id AS Id, usuario_id AS UsuarioId, nombre AS Nombre, presupuesto_maximo AS PresupuestoMaximo, cantidad_maxima_jugadores AS CantidadMaximaJugadores FROM plantillas WHERE id = @id";
        return conn.QueryFirstOrDefault<Plantilla>(sql, new { id });
    }

    public int Crear(Plantilla plantilla)
    {
        using var conn = _db.CreateConnection();
        string sql = @"INSERT INTO plantillas (usuario_id, nombre, presupuesto_maximo, cantidad_maxima_jugadores) 
                       VALUES (@UsuarioId, @Nombre, @PresupuestoMaximo, @CantidadMaximaJugadores);
                       SELECT LAST_INSERT_ID();";
        return conn.ExecuteScalar<int>(sql, plantilla);
    }
}
