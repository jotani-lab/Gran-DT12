using ProyectoCore.Models;
using ProyectoCore.Repositories.RepoClase;
using Xunit;

namespace ProyectoCore.Test.TestRepositories;

public class TestRepoEquipo
{
    [MySqlFact]
    public void CrearYConsultar_UsaTablaEquipos()
    {
        using var db = new TestRepositorioSupport();
        var repo = new RepoEquipo(db.Db);
        var equipo = new Equipo { Nombre = $"Equipo-{Guid.NewGuid():N}" };

        var id = repo.Crear(equipo);
        db.RegistrarEquipo(id);
        var obtenido = repo.ObtenerPorId(id);

        Assert.NotNull(obtenido);
        Assert.Equal(equipo.Nombre, obtenido.Nombre);
        Assert.Contains(repo.ObtenerTodos(), e => e.Id == id);

    }
}
