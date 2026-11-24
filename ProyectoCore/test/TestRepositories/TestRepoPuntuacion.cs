using ProyectoCore.Models;
using ProyectoCore.Repositories.RepoClase;
using Xunit;

namespace ProyectoCore.Test.TestRepositories;

public class TestRepoPuntuacion
{
    [MySqlFact]
    public void RegistrarYConsultar_UsaTablaPuntuacion()
    {
        using var db = new TestRepositorioSupport();
        var equipoId = db.CrearEquipo();
        var posicionId = db.CrearPosicion();
        var jugadorId = db.CrearJugador(equipoId, posicionId);
        var repo = new RepoPuntuacion(db.Connection);
        var puntuacion = new Puntuacion
        {
            JugadorId = jugadorId,
            NroMes = 11,
            NroFecha = 1,
            Nota = 8.5m
        };

        Assert.True(repo.RegistrarPuntuacion(puntuacion));
        Assert.True(puntuacion.IdPuntuacion > 0);

        var obtenidas = repo.ObtenerPorJugadorId(jugadorId);
        var registrada = Assert.Single(obtenidas);
        Assert.True(registrada.IdPuntuacion > 0);
        Assert.Equal(11, registrada.NroMes);
        Assert.Equal(1, registrada.NroFecha);
        Assert.Equal(8.5m, registrada.Nota);
        Assert.False(string.IsNullOrWhiteSpace(registrada.NombreJugador));
    }
}
