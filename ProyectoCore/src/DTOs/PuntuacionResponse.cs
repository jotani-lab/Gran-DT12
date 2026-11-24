using ProyectoCore.Models;

namespace ProyectoCore.DTOs;

public class PuntuacionResponse
{
    public int IdPuntuacion { get; set; }
    public int JugadorId { get; set; }
    public string NombreJugador { get; set; } = string.Empty;
    public int NroMes { get; set; }
    public int NroFecha { get; set; }
    public decimal Nota { get; set; }

    public static PuntuacionResponse DesdePuntuacion(Puntuacion puntuacion) => new()
    {
        IdPuntuacion = puntuacion.IdPuntuacion,
        JugadorId = puntuacion.JugadorId,
        NombreJugador = puntuacion.NombreJugador,
        NroMes = puntuacion.NroMes,
        NroFecha = puntuacion.NroFecha,
        Nota = puntuacion.Nota
    };
}
