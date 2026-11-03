namespace ProyectoCore.Models;

public class Plantilla
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Nombre { get; set; } = "Mi Plantilla";
    public decimal PresupuestoMaximo { get; set; } = 100000000.00m;
    public int CantidadMaximaJugadores { get; set; } = 20;

}
