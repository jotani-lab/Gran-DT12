using Dapper;
using System.Data;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoPlantilla : IRepoPlantilla
{
    private readonly IDbConnection _connection;
    public RepoPlantilla(IDbConnection connection) { _connection = connection; }

    public List<Plantilla> ObtenerPorUsuarioId(int usuarioId)
    {
        string sql = "SELECT id AS Id, usuarioId AS UsuarioId, nombre AS Nombre, presupuestoMaximo AS Presupuesto, cantidadMaximaJugadores AS CantidadJugadores FROM Plantilla WHERE usuarioId = @usuarioId";
        return _connection.Query<Plantilla>(sql, new { usuarioId }).ToList();
    }

    public Plantilla? ObtenerPorId(int id)
    {
        string sql = "SELECT id AS Id, usuarioId AS UsuarioId, nombre AS Nombre, presupuestoMaximo AS Presupuesto, cantidadMaximaJugadores AS CantidadJugadores FROM Plantilla WHERE id = @id";
        return _connection.QueryFirstOrDefault<Plantilla>(sql, new { id });
    }

    public decimal PuntajeTitularesFecha(int plantillaId, int nroMes, int nroFecha)
    {
        const string sql = @"
            SELECT COALESCE(SUM(p.nota), 0)
            FROM PlantillaTitular pt
            INNER JOIN Puntuacion p ON p.jugadorId = pt.jugadorId
            WHERE pt.plantillaId = @plantillaId
              AND p.nroMes = @nroMes
              AND p.nroFecha = @nroFecha";

        return _connection.ExecuteScalar<decimal>(sql, new { plantillaId, nroMes, nroFecha });
    }

    public int Crear(Plantilla plantilla)
    {
        string sql = @"INSERT INTO Plantilla (usuarioId, nombre, presupuestoMaximo, cantidadMaximaJugadores)
                       VALUES (@UsuarioId, @Nombre, @Presupuesto, @CantidadJugadores);
                       SELECT LAST_INSERT_ID();";
        return _connection.ExecuteScalar<int>(sql, plantilla);
    }

    public int BorrarPorId(Plantilla plantilla)
    {
        string sql = "DELETE FROM Plantilla WHERE id = @Id";
        return _connection.Execute(sql, plantilla);
    }
}
