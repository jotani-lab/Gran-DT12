namespace ProyectoCore.Models;

public class PlantillaSuplente
{
    public int PlantillaId { get; set; }
    public int JugadorId { get; set; }
    public Jugador? Jugador { get; set; }

    public bool Verificar()
    {
        if (PlantillaId <= 0)
            return false;

        if (JugadorId <= 0)
            return false;

        return true;
    }
}
