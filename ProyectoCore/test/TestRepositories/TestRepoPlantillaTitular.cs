using ProyectoCore.Repositories.RepoClase;
using Xunit;

namespace ProyectoCore.Test.TestRepositories;

public class TestRepoPlantillaTitular
{
    [MySqlFact]
    public void AgregarYRemoverTitular_UsaTablaPlantillaTitulares()
    {
        using var db = new TestRepositorioSupport();
        var equipoId = db.CrearEquipo();
        var posicionId = db.CrearPosicion();
        var jugadorId = db.CrearJugador(equipoId, posicionId);
        var usuarioId = db.CrearUsuario();
        var plantillaId = db.CrearPlantilla(usuarioId);
        var repo = new RepoPlantillaTitular(db.Db);

        Assert.True(repo.AgregarTitular(plantillaId, jugadorId));
        Assert.True(repo.RemoverTitular(plantillaId, jugadorId));
    }
}
