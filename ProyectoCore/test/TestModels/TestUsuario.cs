using ProyectoCore.Security;
using Xunit;

namespace ProyectoCore.Test.TestModels;

public class TestUsuario
{
    private readonly Encriptador256 _encriptador = new();

    [Fact]
    public void Encriptador256_Encriptar_GeneraHashDe64Caracteres()
    {
        var hash = _encriptador.Encriptar("Password123!");

        Assert.Equal(64, hash.Length);
    }

    [Fact]
    public void Encriptador256_Validar_ContraseñaCorrectaRetornaTrue()
    {
        var hash = _encriptador.Encriptar("SecretKey2026");

        Assert.True(_encriptador.Validar("SecretKey2026", hash));
        Assert.False(_encriptador.Validar("WrongKey", hash));
    }

    [Fact]
    public void Encriptador256_Validar_HashMalformadoRetornaFalse()
    {
        Assert.False(_encriptador.Validar("SecretKey2026", "no-es-un-hash"));
    }
}
