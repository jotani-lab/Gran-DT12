using ProyectoCore.Models;
using Xunit;

namespace ProyectoCore.Test.TestModels;

public class TestPlantillaSuplente
{
    [Fact]
    public void PlantillaSuplente_PropiedadesCorrectas()
    {
        var ps = new PlantillaSuplente { PlantillaId = 1, JugadorId = 12 };
        Assert.Equal(1, ps.PlantillaId);
        Assert.Equal(12, ps.JugadorId);
    }
}
