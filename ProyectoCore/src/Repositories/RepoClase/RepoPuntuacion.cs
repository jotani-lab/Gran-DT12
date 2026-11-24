using Dapper;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoPuntuacion : IRepoPuntuacion
{
    private readonly DBConnection _db;
    public RepoPuntuacion(DBConnection db) { _db = db; }

    public bool RegistrarPuntuacion(Puntuacion puntuacion)
    {
        using var conn = _db.CreateConnection();
        string sql = @"INSERT INTO puntuaciones (jugador_id, nro_fecha, nota) 
                       VALUES (@JugadorId, @NroFecha, @Nota)";
        int rows = conn.Execute(sql, puntuacion);
        if (rows > 0)
            puntuacion.Id = checked((int)conn.ExecuteScalar<long>("SELECT LAST_INSERT_ID()"));

        return rows > 0;
    }

    public List<Puntuacion> ObtenerPorJugadorId(int jugadorId)
    {
        using var conn = _db.CreateConnection();
        string sql = "SELECT id AS Id, jugador_id AS JugadorId, nro_fecha AS NroFecha, nota AS Nota FROM puntuaciones WHERE jugador_id = @jugadorId";
        return conn.Query<Puntuacion>(sql, new { jugadorId }).ToList();
    }
}
