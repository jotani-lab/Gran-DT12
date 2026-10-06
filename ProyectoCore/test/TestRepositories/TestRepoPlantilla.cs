using ProyectoCore.Models;
using ProyectoCore.Repositories.RepoClase;
using Xunit;

namespace ProyectoCore.Test.TestRepositories;

public class TestRepoPlantilla
{
    [MySqlFact]
    public void CrearYConsultar_PersisteCantidadMaximaJugadores()
    {
        using var db = new TestRepositorioSupport();
        var usuarioId = db.CrearUsuario();
        var repo = new RepoPlantilla(db.Db);
        var plantilla = new Plantilla
        {
            UsuarioId = usuarioId,
            Nombre = "Plantilla de integración",
            PresupuestoMaximo = 250000m,
            CantidadMaximaJugadores = 18
        };

        var id = repo.Crear(plantilla);
        db.RegistrarPlantilla(id);

        Assert.Equal(18, repo.ObtenerPorId(id)?.CantidadMaximaJugadores);
        Assert.Equal(18, repo.ObtenerPorUsuarioId(usuarioId)?.CantidadMaximaJugadores);
    }
}
