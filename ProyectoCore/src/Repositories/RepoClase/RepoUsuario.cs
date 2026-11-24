using Dapper;
using System.Data;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoUsuario : IRepoUsuario
{
    private readonly IDbConnection _connection;
    public RepoUsuario(IDbConnection connection) { _connection = connection; }

    public List<Usuario> ObtenerTodos()
    {
        string sql = "SELECT id AS Id, nombre AS Nombre, apellido AS Apellido, email AS Email, fechaNacimiento AS FechaNacimiento, passwordHash AS PasswordHash, esAdmin AS EsAdmin FROM Usuario";
        return _connection.Query<Usuario>(sql).ToList();
    }

    public Usuario? ObtenerPorEmail(string email)
    {
        string sql = "SELECT id AS Id, nombre AS Nombre, apellido AS Apellido, email AS Email, fechaNacimiento AS FechaNacimiento, passwordHash AS PasswordHash, esAdmin AS EsAdmin FROM Usuario WHERE email = @email";
        return _connection.QueryFirstOrDefault<Usuario>(sql, new { email });
    }

    public Usuario? ObtenerPorId(int id)
    {
        string sql = "SELECT id AS Id, nombre AS Nombre, apellido AS Apellido, email AS Email, fechaNacimiento AS FechaNacimiento, passwordHash AS PasswordHash, esAdmin AS EsAdmin FROM Usuario WHERE id = @id";
        return _connection.QueryFirstOrDefault<Usuario>(sql, new { id });
    }

    public int Crear(Usuario usuario)
    {
        string sql = @"INSERT INTO Usuario (nombre, apellido, email, fechaNacimiento, passwordHash, esAdmin)
                       VALUES (@Nombre, @Apellido, @Email, @FechaNacimiento, @PasswordHash, @EsAdmin);
                       SELECT LAST_INSERT_ID();";
        return _connection.ExecuteScalar<int>(sql, usuario);
    }

    public int BorrarPorId(Usuario usuario)
    {
        const string sql = "DELETE FROM Usuario WHERE id = @Id";
        return _connection.Execute(sql, usuario);
    }
}
