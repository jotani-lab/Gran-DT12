using System.Data;
using MySqlConnector;

namespace ProyectoCore.Repositories.RepoClase;

public class DBConnection
{
    private readonly string _connectionString;
    public DBConnection(string connectionString) { _connectionString = connectionString; }
    public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
}
