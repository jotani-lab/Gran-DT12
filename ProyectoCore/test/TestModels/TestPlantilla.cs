using ProyectoCore.Models;
using Xunit;

namespace ProyectoCore.Test.TestModels;

public class TestPlantilla
{
    [Fact]
    public void Plantilla_CantidadMaximaJugadores_UsaValorPredeterminadoYPermiteConfiguracion()
    {
        var plantilla = new Plantilla();

        Assert.Equal(20, plantilla.CantidadJugadores);

        plantilla.CantidadJugadores = 12;
        Assert.Equal(12, plantilla.CantidadJugadores);
    }

}
