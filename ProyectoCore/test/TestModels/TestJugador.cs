using ProyectoCore.Models;
using Xunit;

namespace ProyectoCore.Test.TestModels;

public class TestJugador
{
    [Fact]
    public void Jugador_CotizacionValida_AsignaCorrectamente()
    {
        var j = new Jugador { Cotizacion = 5000000.00m };
        Assert.Equal(5000000.00m, j.Cotizacion);
    }

    [Fact]
    public void Jugador_CotizacionExcesiva_LanzaExcepcion()
    {
        var j = new Jugador();
        Assert.Throws<ArgumentOutOfRangeException>(() => j.Cotizacion = 100000000.00m);
    }

    [Fact]
    public void Jugador_PuntuacionesMismaFecha_LanzaExcepcion()
    {
        var j = new Jugador();
        j.AgregarPuntuacion(new Puntuacion { NroFecha = 1, Nota = 8.0m });
        Assert.Throws<InvalidOperationException>(() => j.AgregarPuntuacion(new Puntuacion { NroFecha = 1, Nota = 7.0m }));
    }
}
