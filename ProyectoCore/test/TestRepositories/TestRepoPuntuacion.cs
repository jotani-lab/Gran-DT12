using ProyectoCore.Models;
using ProyectoCore.Repositories.RepoClase;
using Xunit;

namespace ProyectoCore.Test.TestRepositories;

public class TestRepoPuntuacion
{
    [MySqlFact]
    public void RegistrarYConsultar_UsaTablaPuntuaciones()
    {
        using var db = new TestRepositorioSupport();
        var equipoId = db.CrearEquipo();
        var posicionId = db.CrearPosicion();
        var jugadorId = db.CrearJugador(equipoId, posicionId);
        var repo = new RepoPuntuacion(db.Db);
        var puntuacion = new Puntuacion
        {
            JugadorId = jugadorId,
            NroFecha = 1,
            Nota = 8.5m
        };

        Assert.True(repo.RegistrarPuntuacion(puntuacion));
        var obtenidas = repo.ObtenerPorJugadorId(jugadorId);

        var registrada = Assert.Single(obtenidas);
        Assert.Equal(1, registrada.NroFecha);
        Assert.Equal(8.5m, registrada.Nota);
    }
}
