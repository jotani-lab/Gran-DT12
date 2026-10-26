using ProyectoCore.Models;
using Xunit;

namespace ProyectoCore.Test.TestModels;

public class TestEquipo
{
    [Fact]
    public void Equipo_AsignaPropiedadesCorrectamente()
    {
        var e = new Equipo { Id = 1, Nombre = "Boca Juniors" };
        Assert.Equal(1, e.Id);
        Assert.Equal("Boca Juniors", e.Nombre);
    }
}
