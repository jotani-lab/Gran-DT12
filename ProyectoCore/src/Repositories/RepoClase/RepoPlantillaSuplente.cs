using Dapper;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoPlantillaSuplente : IRepoPlantillaSuplente
{
    private readonly DBConnection _db;
    public RepoPlantillaSuplente(DBConnection db) { _db = db; }

    public bool AgregarSuplente(int plantillaId, int jugadorId)
    {
        using var conn = _db.CreateConnection();
        string sql = "INSERT INTO PlantillaSuplente (plantillaId, jugadorId) VALUES (@plantillaId, @jugadorId)";
        int rows = conn.Execute(sql, new { plantillaId, jugadorId });
        return rows > 0;
    }

    public bool RemoverSuplente(int plantillaId, int jugadorId)
    {
        using var conn = _db.CreateConnection();
        string sql = "DELETE FROM PlantillaSuplente WHERE plantillaId = @plantillaId AND jugadorId = @jugadorId";
        int rows = conn.Execute(sql, new { plantillaId, jugadorId });
        return rows > 0;
    }
}
