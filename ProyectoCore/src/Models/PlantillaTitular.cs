namespace ProyectoCore.Models;

public class PlantillaTitular
{
    public int PlantillaId { get; set; }
    public int JugadorId { get; set; }
    public Jugador? Jugador { get; set; }

}
