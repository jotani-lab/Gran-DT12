using ProyectoCore.Models;
using Xunit;

namespace ProyectoCore.Test.TestModels;

public class TestPlantillaTitular
{
    [Fact]
    public void PlantillaTitular_PropiedadesCorrectas()
    {
        var pt = new PlantillaTitular { PlantillaId = 1, JugadorId = 5 };
        Assert.Equal(1, pt.PlantillaId);
        Assert.Equal(5, pt.JugadorId);
    }
}
