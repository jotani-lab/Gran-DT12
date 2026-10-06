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
}
