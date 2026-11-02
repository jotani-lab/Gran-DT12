namespace ProyectoCore.Models;

public class PlantillaSuplente
{
    public int PlantillaId { get; set; }
    public int JugadorId { get; set; }
    public Jugador? Jugador { get; set; }

//TODO pasar a service
    
}
