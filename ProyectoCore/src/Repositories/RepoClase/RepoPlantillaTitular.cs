using Dapper;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoPlantillaTitular : IRepoPlantillaTitular
{
    private readonly DBConnection _db;
    public RepoPlantillaTitular(DBConnection db) { _db = db; }

    public bool AgregarTitular(int plantillaId, int jugadorId)
    {
        using var conn = _db.CreateConnection();
        string sql = "INSERT INTO plantilla_titulares (plantilla_id, jugador_id) VALUES (@plantillaId, @jugadorId)";
        int rows = conn.Execute(sql, new { plantillaId, jugadorId });
        return rows > 0;
    }

    public bool RemoverTitular(int plantillaId, int jugadorId)
    {
        using var conn = _db.CreateConnection();
        string sql = "DELETE FROM plantilla_titulares WHERE plantilla_id = @plantillaId AND jugador_id = @jugadorId";
        int rows = conn.Execute(sql, new { plantillaId, jugadorId });
        return rows > 0;
    }
}
