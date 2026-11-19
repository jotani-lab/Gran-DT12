namespace ProyectoCore.Models;

public class Plantilla
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Nombre { get; set; } = "Mi Plantilla";
    public decimal Presupuesto { get; set; } = 100000000.00m;
    public int CantidadJugadores { get; set; } = 20;

}
