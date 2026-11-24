using ProyectoCore.Models;
using ProyectoCore.Repositories.RepoClase;
using Dapper;
using Xunit;

namespace ProyectoCore.Test.TestRepositories;

public class TestRepoPlantilla
{
    [MySqlFact]
    public void CrearYConsultar_PersisteCantidadMaximaJugadores()
    {
        using var db = new TestRepositorioSupport();
        var usuarioId = db.CrearUsuario();
        var repo = new RepoPlantilla(db.Connection);
        var plantilla = new Plantilla
        {
            UsuarioId = usuarioId,
            Nombre = "Plantilla de integración",
            Presupuesto = 250000m,
            CantidadJugadores = 18
        };

        var id = repo.Crear(plantilla);
        db.RegistrarPlantilla(id);

        Assert.Equal(18, repo.ObtenerPorId(id)?.CantidadJugadores);
        Assert.Equal(18, Assert.Single(repo.ObtenerPorUsuarioId(usuarioId)).CantidadJugadores);
    }

    [MySqlFact]
    public void PuntajeTitularesFecha_SumaSoloLaFechaYMesSolicitados()
    {
        using var db = new TestRepositorioSupport();
        var usuarioId = db.CrearUsuario();
        var equipoId = db.CrearEquipo();
        var posicionId = db.CrearPosicion();
        var jugadorId = db.CrearJugador(equipoId, posicionId);
        var plantillaId = db.CrearPlantilla(usuarioId);
        db.Connection.Execute(
            "INSERT INTO PlantillaTitular (plantillaId, jugadorId) VALUES (@plantillaId, @jugadorId)",
            new { plantillaId, jugadorId });
        db.Connection.Execute(
            "INSERT INTO Puntuacion (jugadorId, nroMes, nroFecha, nota) VALUES (@jugadorId, 5, 12, 8.5), (@jugadorId, 5, 13, 9.0), (@jugadorId, 6, 12, 10.0)",
            new { jugadorId });

        var repo = new RepoPlantilla(db.Connection);

        Assert.Equal(8.5m, repo.PuntajeTitularesFecha(plantillaId, 5, 12));
        Assert.Equal(0m, repo.PuntajeTitularesFecha(plantillaId, 7, 12));
    }
}
