using ProyectoCore.Repositories.RepoClase;
using Xunit;

namespace ProyectoCore.Test.TestRepositories;

public class TestRepoPosicion
{
    [MySqlFact]
    public void ConsultarPosicion_LeeLaTablaPosicion()
    {
        using var db = new TestRepositorioSupport();
        var id = db.CrearPosicion();
        var repo = new RepoPosicion(db.Connection);

        var posicion = repo.ObtenerPorId(id);

        Assert.NotNull(posicion);
        Assert.Contains(repo.ObtenerTodas(), p => p.Id == id);
    }
}
