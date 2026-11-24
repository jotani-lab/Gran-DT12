using Dapper;
using MySqlConnector;
using Xunit;

namespace ProyectoCore.Test.TestRepositories;

public sealed class MySqlFactAttribute : FactAttribute
{
    public MySqlFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("GRANDT_TEST_CONNECTION_STRING")))
            Skip = "Definí GRANDT_TEST_CONNECTION_STRING para ejecutar las pruebas de integración con MySQL.";
    }
}

public sealed class TestRepositorioSupport : IDisposable
{
    private readonly List<int> _equipoIds = new();
    private readonly List<int> _posicionIds = new();
    private readonly List<int> _usuarioIds = new();
    private readonly List<int> _jugadorIds = new();
    private readonly List<int> _plantillaIds = new();

    public MySqlConnection Connection { get; }

    public void RegistrarEquipo(int id) => _equipoIds.Add(id);
    public void RegistrarUsuario(int id) => _usuarioIds.Add(id);
    public void RegistrarJugador(int id) => _jugadorIds.Add(id);
    public void RegistrarPlantilla(int id) => _plantillaIds.Add(id);

    public TestRepositorioSupport()
    {
        var connectionString = Environment.GetEnvironmentVariable("GRANDT_TEST_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Falta configurar GRANDT_TEST_CONNECTION_STRING.");

        var settings = new MySqlConnectionStringBuilder(connectionString);
        if (!settings.Database.EndsWith("_test", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Las pruebas SQL solo pueden usar una base cuyo nombre termine en '_test'.");

        Connection = new MySqlConnection(connectionString);
        Connection.Open();
    }

    public int CrearEquipo()
    {
        var id = Connection.ExecuteScalar<int>(
            "INSERT INTO Equipo (nombre) VALUES (@nombre); SELECT LAST_INSERT_ID();",
            new { nombre = $"Equipo-{Guid.NewGuid():N}" });
        _equipoIds.Add(id);
        return id;
    }

    public int CrearPosicion()
    {
        var id = Connection.ExecuteScalar<int>(
            "INSERT INTO Posicion (nombre) VALUES (@nombre); SELECT LAST_INSERT_ID();",
            new { nombre = $"Posicion-{Guid.NewGuid():N}" });
        _posicionIds.Add(id);
        return id;
    }

    public int CrearUsuario()
    {
        var id = Connection.ExecuteScalar<int>(
                        @"INSERT INTO Usuario (nombre, apellido, email, fechaNacimiento, passwordHash)
                            VALUES (@nombre, 'Integracion', @email, '1990-01-01', REPEAT('a', 64));
              SELECT LAST_INSERT_ID();",
            new
            {
                nombre = $"Usuario-{Guid.NewGuid():N}",
                email = $"{Guid.NewGuid():N}@example.test"
            });
        _usuarioIds.Add(id);
        return id;
    }

    public int CrearJugador(int equipoId, int posicionId)
    {
        var id = Connection.ExecuteScalar<int>(
            @"INSERT INTO Jugador (nombre, apellido, fechaNacimiento, equipoId, posicionId, cotizacion)
              VALUES (@nombre, 'Integracion', '1990-01-01', @equipoId, @posicionId, 1234.56);
              SELECT LAST_INSERT_ID();",
            new
            {
                nombre = $"Jugador-{Guid.NewGuid():N}",
                equipoId,
                posicionId
            });
        _jugadorIds.Add(id);
        return id;
    }

    public int CrearPlantilla(int usuarioId, int cantidadMaximaJugadores = 20)
    {
        var id = Connection.ExecuteScalar<int>(
            @"INSERT INTO Plantilla (usuarioId, nombre, cantidadMaximaJugadores)
              VALUES (@usuarioId, @nombre, @cantidadMaximaJugadores);
              SELECT LAST_INSERT_ID();",
            new
            {
                usuarioId,
                nombre = $"Plantilla-{Guid.NewGuid():N}",
                cantidadMaximaJugadores
            });
        _plantillaIds.Add(id);
        return id;
    }

    public void Dispose()
    {
        try
        {
            if (_jugadorIds.Count > 0)
            {
                Connection.Execute("DELETE FROM Puntuacion WHERE jugadorId IN @ids", new { ids = _jugadorIds });
                Connection.Execute("DELETE FROM PlantillaTitular WHERE jugadorId IN @ids", new { ids = _jugadorIds });
                Connection.Execute("DELETE FROM PlantillaSuplente WHERE jugadorId IN @ids", new { ids = _jugadorIds });
            }

            if (_plantillaIds.Count > 0)
            {
                Connection.Execute("DELETE FROM PlantillaTitular WHERE plantillaId IN @ids", new { ids = _plantillaIds });
                Connection.Execute("DELETE FROM PlantillaSuplente WHERE plantillaId IN @ids", new { ids = _plantillaIds });
                Connection.Execute("DELETE FROM Plantilla WHERE id IN @ids", new { ids = _plantillaIds });
            }

            if (_jugadorIds.Count > 0)
                Connection.Execute("DELETE FROM Jugador WHERE id IN @ids", new { ids = _jugadorIds });
            if (_usuarioIds.Count > 0)
                Connection.Execute("DELETE FROM Usuario WHERE id IN @ids", new { ids = _usuarioIds });
            if (_equipoIds.Count > 0)
                Connection.Execute("DELETE FROM Equipo WHERE id IN @ids", new { ids = _equipoIds });
            if (_posicionIds.Count > 0)
                Connection.Execute("DELETE FROM Posicion WHERE id IN @ids", new { ids = _posicionIds });
        }
        finally
        {
            Connection.Dispose();
        }
    }
}
