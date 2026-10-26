using ProyectoCore.Repositories.RepoClase;
using Xunit;

namespace ProyectoCore.Test.TestRepositories;

public class TestRepoPlantillaSuplente
{
    [MySqlFact]
    public void AgregarYRemoverSuplente_UsaTablaPlantillaSuplentes()
    {
        using var db = new TestRepositorioSupport();
        var equipoId = db.CrearEquipo();
        var posicionId = db.CrearPosicion();
        var jugadorId = db.CrearJugador(equipoId, posicionId);
        var usuarioId = db.CrearUsuario();
        var plantillaId = db.CrearPlantilla(usuarioId);
        var repo = new RepoPlantillaSuplente(db.Db);

        Assert.True(repo.AgregarSuplente(plantillaId, jugadorId));
        Assert.True(repo.RemoverSuplente(plantillaId, jugadorId));
    }
}
