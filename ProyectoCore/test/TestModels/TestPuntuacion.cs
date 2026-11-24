using ProyectoCore.Models;
using Xunit;

namespace ProyectoCore.Test.TestModels;

public class TestPuntuacion
{
    [Fact]
    public void Puntuacion_NotaInvalida_LanzaExcepcion()
    {
        var p = new Puntuacion();
        Assert.Throws<ArgumentOutOfRangeException>(() => p.Nota = 11.0m);
        Assert.Throws<ArgumentOutOfRangeException>(() => p.Nota = 0.5m);
    }

    [Fact]
    public void Puntuacion_FechaInvalida_LanzaExcepcion()
    {
        var p = new Puntuacion();
        Assert.Throws<ArgumentOutOfRangeException>(() => p.NroFecha = 50);
        Assert.Throws<ArgumentOutOfRangeException>(() => p.NroFecha = 0);
    }

    [Fact]
    public void Puntuacion_MesInvalido_LanzaExcepcion()
    {
        var p = new Puntuacion();
        Assert.Throws<ArgumentOutOfRangeException>(() => p.NroMes = 0);
        Assert.Throws<ArgumentOutOfRangeException>(() => p.NroMes = 13);
    }

    [Fact]
    public void Puntuacion_IdUsaIdPuntuacion()
    {
        var p = new Puntuacion { IdPuntuacion = 25 };
        Assert.Equal(25, p.IdPuntuacion);
        Assert.Equal(25, p.Id);

        p.Id = 30;
        Assert.Equal(30, p.IdPuntuacion);
    }
}
