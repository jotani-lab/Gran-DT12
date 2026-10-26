using Dapper;
using Xunit;

namespace ProyectoCore.Test.TestRepositories;

public class TestDBConnection
{
    [MySqlFact]
    public void CreateConnection_AbreConexionConLaBaseDePrueba()
    {
        using var db = new TestRepositorioSupport();

        Assert.True(db.Connection.State == System.Data.ConnectionState.Open);
        Assert.Equal(1, db.Connection.ExecuteScalar<int>("SELECT 1"));
    }
}
