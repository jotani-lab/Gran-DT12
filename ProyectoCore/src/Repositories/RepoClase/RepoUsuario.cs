using Dapper;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoUsuario : IRepoUsuario
{
    private readonly DBConnection _db;
    public RepoUsuario(DBConnection db) { _db = db; }

    public List<Usuario> ObtenerTodos()
    {
        using var conn = _db.CreateConnection();
        string sql = "SELECT id AS Id, nombre AS Nombre, apellido AS Apellido, email AS Email, fecha_nacimiento AS FechaNacimiento, password_hash AS PasswordHash, es_admin AS EsAdmin FROM usuarios";
        return conn.Query<Usuario>(sql).ToList();
    }

    public Usuario? ObtenerPorEmail(string email)
    {
        using var conn = _db.CreateConnection();
        string sql = "SELECT id AS Id, nombre AS Nombre, apellido AS Apellido, email AS Email, fecha_nacimiento AS FechaNacimiento, password_hash AS PasswordHash, es_admin AS EsAdmin FROM usuarios WHERE email = @email";
        return conn.QueryFirstOrDefault<Usuario>(sql, new { email });
    }

    public Usuario? ObtenerPorId(int id)
    {
        using var conn = _db.CreateConnection();
        string sql = "SELECT id AS Id, nombre AS Nombre, apellido AS Apellido, email AS Email, fecha_nacimiento AS FechaNacimiento, password_hash AS PasswordHash, es_admin AS EsAdmin FROM usuarios WHERE id = @id";
        return conn.QueryFirstOrDefault<Usuario>(sql, new { id });
    }

    public int Crear(Usuario usuario)
    {
        using var conn = _db.CreateConnection();
        string sql = @"INSERT INTO usuarios (nombre, apellido, email, fecha_nacimiento, password_hash, es_admin)
                       VALUES (@Nombre, @Apellido, @Email, @FechaNacimiento, @PasswordHash, @EsAdmin);
                       SELECT LAST_INSERT_ID();";
        return conn.ExecuteScalar<int>(sql, usuario);
    }
}
