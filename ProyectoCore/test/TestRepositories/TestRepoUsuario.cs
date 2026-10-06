using ProyectoCore.Models;
using ProyectoCore.Repositories.RepoClase;
using Xunit;

namespace ProyectoCore.Test.TestRepositories;

public class TestRepoUsuario
{
    [MySqlFact]
    public void CrearYConsultar_UsaTablaUsuarios()
    {
        using var db = new TestRepositorioSupport();
        var repo = new RepoUsuario(db.Db);
        var usuario = new Usuario
        {
            Nombre = $"Usuario-{Guid.NewGuid():N}",
            Apellido = "Integracion",
            Email = $"{Guid.NewGuid():N}@example.test",
            FechaNacimiento = new DateTime(1990, 1, 1),
            PasswordHash = new string('a', 64)
        };

        var id = repo.Crear(usuario);
        db.RegistrarUsuario(id);
        var obtenido = repo.ObtenerPorId(id);

        Assert.NotNull(obtenido);
        Assert.Equal(usuario.Email, obtenido.Email);
        Assert.Equal(id, repo.ObtenerPorEmail(usuario.Email)?.Id);
        Assert.Contains(repo.ObtenerTodos(), u => u.Id == id);

    }
}
