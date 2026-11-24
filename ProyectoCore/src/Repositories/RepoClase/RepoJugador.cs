using Dapper;
using System.Data;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Repositories.RepoClase;

public class RepoJugador : IRepoJugador
{
    private readonly IDbConnection _connection;
    public RepoJugador(IDbConnection connection) { _connection = connection; }

    public List<Jugador> ObtenerTodos()
    {
        string sql = @"
            SELECT j.id AS Id, j.nombre AS Nombre, j.apellido AS Apellido, j.apodo AS Apodo, 
                   j.fechaNacimiento AS FechaNacimiento, j.equipoId AS EquipoId, j.posicionId AS PosicionId,
                   j.cotizacion AS Cotizacion, e.id AS Id, e.nombre AS Nombre, p.id AS Id, p.nombre AS Nombre
            FROM Jugador j
            INNER JOIN Equipo e ON j.equipoId = e.id
            INNER JOIN Posicion p ON j.posicionId = p.id";

        return _connection.Query<Jugador, Equipo, Posicion, Jugador>(sql, (jugador, equipo, posicion) =>
        {
            jugador.Equipo = equipo;
            jugador.Posicion = posicion;
            return jugador;
        }, splitOn: "Id,Id").ToList();
    }

    public Jugador? ObtenerPorId(int id)
    {
        string sql = @"
            SELECT j.id AS Id, j.nombre AS Nombre, j.apellido AS Apellido, j.apodo AS Apodo, 
                   j.fechaNacimiento AS FechaNacimiento, j.equipoId AS EquipoId, j.posicionId AS PosicionId,
                   j.cotizacion AS Cotizacion, e.id AS Id, e.nombre AS Nombre, p.id AS Id, p.nombre AS Nombre
            FROM Jugador j
            INNER JOIN Equipo e ON j.equipoId = e.id
            INNER JOIN Posicion p ON j.posicionId = p.id
            WHERE j.id = @id";

        var res = _connection.Query<Jugador, Equipo, Posicion, Jugador>(sql, (jugador, equipo, posicion) =>
        {
            jugador.Equipo = equipo;
            jugador.Posicion = posicion;
            return jugador;
        }, new { id }, splitOn: "Id,Id");

        return res.FirstOrDefault();
    }

    public int Crear(Jugador jugador)
    {
        string sql = @"INSERT INTO Jugador (nombre, apellido, apodo, fechaNacimiento, equipoId, posicionId, cotizacion)
                       VALUES (@Nombre, @Apellido, @Apodo, @FechaNacimiento, @EquipoId, @PosicionId, @Cotizacion);
                       SELECT LAST_INSERT_ID();";
        return _connection.ExecuteScalar<int>(sql, jugador);
    }

    public int BorrarPorId(Jugador jugador)
    {
        string sql = @"DELETE FROM Jugador
                       WHERE id = @Id";
        return _connection.Execute(sql, jugador);
    }
}
