namespace ProyectoCore.DTOs;

public class CrearJugadorRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string? Apodo { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    public int EquipoId { get; set; }
    public int PosicionId { get; set; }
    public decimal Cotizacion { get; set; }
}
