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
                   j.fecha_nacimiento AS FechaNacimiento, j.equipo_id AS EquipoId, j.posicion_id AS PosicionId, 
                   j.cotizacion AS Cotizacion, e.id AS Id, e.nombre AS Nombre, p.id AS Id, p.nombre AS Nombre
            FROM jugadores j
            INNER JOIN equipos e ON j.equipo_id = e.id
            INNER JOIN posiciones p ON j.posicion_id = p.id";

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
                   j.fecha_nacimiento AS FechaNacimiento, j.equipo_id AS EquipoId, j.posicion_id AS PosicionId, 
                   j.cotizacion AS Cotizacion, e.id AS Id, e.nombre AS Nombre, p.id AS Id, p.nombre AS Nombre
            FROM jugadores j
            INNER JOIN equipos e ON j.equipo_id = e.id
            INNER JOIN posiciones p ON j.posicion_id = p.id
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
        string sql = @"INSERT INTO jugadores (nombre, apellido, apodo, fecha_nacimiento, equipo_id, posicion_id, cotizacion) 
                       VALUES (@Nombre, @Apellido, @Apodo, @FechaNacimiento, @EquipoId, @PosicionId, @Cotizacion);
                       SELECT LAST_INSERT_ID();";
        return conn.ExecuteScalar<int>(sql, jugador);
    }
}
