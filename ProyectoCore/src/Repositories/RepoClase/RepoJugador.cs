using Dapper;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoJugador : IRepoJugador
{
    private readonly DBConnection _db;
    public RepoJugador(DBConnection db) { _db = db; }

    public List<Jugador> ObtenerTodos()
    {
        using var conn = _db.CreateConnection();
        string sql = @"
            SELECT j.id AS Id, j.nombre AS Nombre, j.apellido AS Apellido, j.apodo AS Apodo, 
                   j.fechaNacimiento AS FechaNacimiento, j.equipoId AS EquipoId, j.posicionId AS PosicionId,
                   j.cotizacion AS Cotizacion, e.id AS Id, e.nombre AS Nombre, p.id AS Id, p.nombre AS Nombre
            FROM Jugador j
            INNER JOIN Equipo e ON j.equipoId = e.id
            INNER JOIN Posicion p ON j.posicionId = p.id";

        return conn.Query<Jugador, Equipo, Posicion, Jugador>(sql, (jugador, equipo, posicion) =>
        {
            jugador.Equipo = equipo;
            jugador.Posicion = posicion;
            return jugador;
        }, splitOn: "Id,Id").ToList();
    }

    public Jugador? ObtenerPorId(int id)
    {
        using var conn = _db.CreateConnection();
        string sql = @"
            SELECT j.id AS Id, j.nombre AS Nombre, j.apellido AS Apellido, j.apodo AS Apodo, 
                   j.fechaNacimiento AS FechaNacimiento, j.equipoId AS EquipoId, j.posicionId AS PosicionId,
                   j.cotizacion AS Cotizacion, e.id AS Id, e.nombre AS Nombre, p.id AS Id, p.nombre AS Nombre
            FROM Jugador j
            INNER JOIN Equipo e ON j.equipoId = e.id
            INNER JOIN Posicion p ON j.posicionId = p.id
            WHERE j.id = @id";

        var res = conn.Query<Jugador, Equipo, Posicion, Jugador>(sql, (jugador, equipo, posicion) =>
        {
            jugador.Equipo = equipo;
            jugador.Posicion = posicion;
            return jugador;
        }, new { id }, splitOn: "Id,Id");

        return res.FirstOrDefault();
    }

    public int Crear(Jugador jugador)
    {
        using var conn = _db.CreateConnection();
        string sql = @"INSERT INTO Jugador (nombre, apellido, apodo, fechaNacimiento, equipoId, posicionId, cotizacion)
                       VALUES (@Nombre, @Apellido, @Apodo, @FechaNacimiento, @EquipoId, @PosicionId, @Cotizacion);
                       SELECT LAST_INSERT_ID();";
        return conn.ExecuteScalar<int>(sql, jugador);
    }

    public int BorrarPorId(Jugador jugador)
    {
        using var conn = _db.CreateConnection();
        string sql = @"DELETE FROM Jugador
                       WHERE id = @Id";
        return conn.Execute(sql, jugador);
    }
}
