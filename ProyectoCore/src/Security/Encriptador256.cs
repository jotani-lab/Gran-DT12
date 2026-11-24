using System.Security.Cryptography;
using System.Text;

namespace ProyectoCore.Security;

public sealed class Encriptador256 : IEncriptador
{
    public string Encriptar(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("La contraseña no puede estar vacía.", nameof(password));

        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    public bool Validar(string password, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordHash))
            return false;

        byte[] storedHash;
        try
        {
            storedHash = Convert.FromHexString(passwordHash);
        }
        catch (FormatException)
        {
            return false;
        }

        byte[] candidateHash = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return storedHash.Length == candidateHash.Length &&
               CryptographicOperations.FixedTimeEquals(storedHash, candidateHash);
    }
}
