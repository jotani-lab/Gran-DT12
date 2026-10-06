using ProyectoCore.Models;
using Xunit;

namespace ProyectoCore.Test.TestModels;

public class TestUsuario
{
    [Fact]
    public void Usuario_EstablecerPassword_GeneraHashDe64Caracteres()
    {
        var u = new Usuario();
        u.EstablecerPassword("Password123!");
        Assert.NotNull(u.PasswordHash);
        Assert.Equal(64, u.PasswordHash.Length);
    }

    [Fact]
    public void Usuario_ValidarPassword_PasswordCorrecta_RetornaTrue()
    {
        var u = new Usuario();
        u.EstablecerPassword("SecretKey2026");
        Assert.True(u.ValidarPassword("SecretKey2026"));
        Assert.False(u.ValidarPassword("WrongKey"));
    }
}
