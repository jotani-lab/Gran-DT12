namespace ProyectoCore.Models;

public class Equipo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    //TODO a capa de servicio
    public bool Verificar()
    {
        if (string.IsNullOrWhiteSpace(Nombre))
            return false;

        return true;
    }

    
}
