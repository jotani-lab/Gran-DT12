using Dapper;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoPuntuacion : IRepoPuntuacion
{
    private readonly DBConnection _db;
    public RepoPuntuacion(DBConnection db) { _db = db; }

    private const string SelectSql = @"
        SELECT p.id AS IdPuntuacion,
               p.jugadorId AS JugadorId,
               CONCAT(j.nombre, ' ', j.apellido) AS NombreJugador,
               p.nroMes AS NroMes,
               p.nroFecha AS NroFecha,
               p.nota AS Nota
        FROM Puntuacion p
        INNER JOIN Jugador j ON j.id = p.jugadorId";

    public bool RegistrarPuntuacion(Puntuacion puntuacion)
    {
        using var conn = _db.CreateConnection();
        const string sql = @"
            INSERT INTO Puntuacion (jugadorId, nroMes, nroFecha, nota)
            VALUES (@JugadorId, @NroMes, @NroFecha, @Nota)
            ON DUPLICATE KEY UPDATE nota = @Nota;
            SELECT id FROM Puntuacion
            WHERE jugadorId = @JugadorId AND nroMes = @NroMes AND nroFecha = @NroFecha;";

        var id = conn.ExecuteScalar<long>(sql, puntuacion);
        if (id > 0)
            puntuacion.IdPuntuacion = checked((int)id);

        return id > 0;
    }

    public bool ActualizarPuntuacion(Puntuacion puntuacion)
    {
        using var conn = _db.CreateConnection();
        const string sql = @"
            UPDATE Puntuacion
            SET jugadorId = @JugadorId, nroMes = @NroMes,
                nroFecha = @NroFecha, nota = @Nota
            WHERE id = @IdPuntuacion";
        return conn.Execute(sql, puntuacion) > 0;
    }

    public bool BorrarPorId(int idPuntuacion)
    {
        using var conn = _db.CreateConnection();
        return conn.Execute("DELETE FROM Puntuacion WHERE id = @idPuntuacion", new { idPuntuacion }) > 0;
    }

    public Puntuacion? ObtenerPorId(int idPuntuacion)
    {
        using var conn = _db.CreateConnection();
        return conn.QueryFirstOrDefault<Puntuacion>(SelectSql + " WHERE p.id = @idPuntuacion", new { idPuntuacion });
    }

    public List<Puntuacion> ObtenerTodas()
    {
        using var conn = _db.CreateConnection();
        return conn.Query<Puntuacion>(SelectSql + " ORDER BY p.nroMes, p.nroFecha, p.id").ToList();
    }

    public List<Puntuacion> ObtenerPorJugadorId(int jugadorId)
    {
        using var conn = _db.CreateConnection();
        return conn.Query<Puntuacion>(
            SelectSql + " WHERE p.jugadorId = @jugadorId ORDER BY p.nroMes, p.nroFecha",
            new { jugadorId }).ToList();
    }

    public List<Puntuacion> ObtenerPorMes(int nroMes)
    {
        using var conn = _db.CreateConnection();
        return conn.Query<Puntuacion>(
            SelectSql + " WHERE p.nroMes = @nroMes ORDER BY p.nroFecha, p.jugadorId",
            new { nroMes }).ToList();
    }

    public Puntuacion? ObtenerPorJugadorMesFecha(int jugadorId, int nroMes, int nroFecha)
    {
        using var conn = _db.CreateConnection();
        return conn.QueryFirstOrDefault<Puntuacion>(
            SelectSql + " WHERE p.jugadorId = @jugadorId AND p.nroMes = @nroMes AND p.nroFecha = @nroFecha",
            new { jugadorId, nroMes, nroFecha });
    }
}
