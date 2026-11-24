using Dapper;
using System.Data;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoPlantillaSuplente : IRepoPlantillaSuplente
{
    private readonly IDbConnection _connection;
    public RepoPlantillaSuplente(IDbConnection connection) { _connection = connection; }

    public bool AgregarSuplente(int plantillaId, int jugadorId)
    {
        string sql = "INSERT INTO PlantillaSuplente (plantillaId, jugadorId) VALUES (@plantillaId, @jugadorId)";
        int rows = _connection.Execute(sql, new { plantillaId, jugadorId });
        return rows > 0;
    }

    public bool RemoverSuplente(int plantillaId, int jugadorId)
    {
        string sql = "DELETE FROM PlantillaSuplente WHERE plantillaId = @plantillaId AND jugadorId = @jugadorId";
        int rows = _connection.Execute(sql, new { plantillaId, jugadorId });
        return rows > 0;
    }
}
