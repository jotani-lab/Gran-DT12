using ProyectoCore.Models;
using Xunit;

namespace ProyectoCore.Test.TestModels;

public class TestPosicion
{
    [Fact]
    public void Posicion_AsignaPropiedades()
    {
        var pos = new Posicion { Id = 1, Nombre = "Arquero" };
        Assert.Equal(1, pos.Id);
        Assert.Equal("Arquero", pos.Nombre);
    }
}
