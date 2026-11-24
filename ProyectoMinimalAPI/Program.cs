using System.Data;
using MySqlConnector;
using ProyectoCore.Repositories.Interfaces;
using ProyectoCore.Repositories.RepoClase;
using ProyectoCore.Security;
using ProyectoCore.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=localhost;Database=5to_GranDT12;User ID=5to_agbd;Password=Trigg3rs!;";

// La conexión pertenece al request y será liberada por el contenedor al terminar el scope.
builder.Services.AddScoped<IDbConnection>(_ => new MySqlConnection(connectionString));

// Registrar Repositorios
builder.Services.AddScoped<IRepoPosicion, RepoPosicion>();
builder.Services.AddScoped<IRepoEquipo, RepoEquipo>();
builder.Services.AddScoped<IRepoJugador, RepoJugador>();
builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();
builder.Services.AddScoped<IRepoPlantilla, RepoPlantilla>();
builder.Services.AddScoped<IRepoPlantillaTitular, RepoPlantillaTitular>();
builder.Services.AddScoped<IRepoPlantillaSuplente, RepoPlantillaSuplente>();
builder.Services.AddScoped<IRepoPuntuacion, RepoPuntuacion>();

// Registrar Servicios
builder.Services.AddSingleton<IEncriptador, Encriptador256>();
builder.Services.AddScoped<ServicePosicion>();
builder.Services.AddScoped<ServiceEquipo>();
builder.Services.AddScoped<ServiceJugador>();
builder.Services.AddScoped<ServiceUsuario>();
builder.Services.AddScoped<ServicePlantilla>();
builder.Services.AddScoped<ServicePlantillaTitular>();
builder.Services.AddScoped<ServicePlantillaSuplente>();
builder.Services.AddScoped<ServicePuntuacion>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("GranDT API - Documentación Scalar")
               .WithTheme(ScalarTheme.BluePlanet);
    });

app.UseAuthorization();
app.MapControllers();

app.Run();
