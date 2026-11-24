namespace ProyectoCore.Security;

public interface IEncriptador
{
    string Encriptar(string password);
    bool Validar(string password, string passwordHash);
}
