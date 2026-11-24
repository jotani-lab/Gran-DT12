using Dapper;
using System.Data;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoPlantillaTitular : IRepoPlantillaTitular
{
    private readonly IDbConnection _connection;
    public RepoPlantillaTitular(IDbConnection connection) { _connection = connection; }

    public bool AgregarTitular(int plantillaId, int jugadorId)
    {
        string sql = "INSERT INTO PlantillaTitular (plantillaId, jugadorId) VALUES (@plantillaId, @jugadorId)";
        int rows = _connection.Execute(sql, new { plantillaId, jugadorId });
        return rows > 0;
    }

    public bool RemoverTitular(int plantillaId, int jugadorId)
    {
        string sql = "DELETE FROM PlantillaTitular WHERE plantillaId = @plantillaId AND jugadorId = @jugadorId";
        int rows = _connection.Execute(sql, new { plantillaId, jugadorId });
        return rows > 0;
    }
}
