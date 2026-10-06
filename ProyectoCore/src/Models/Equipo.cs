namespace ProyectoCore.Models;

public class Equipo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public bool Verificar()
    {
        if (string.IsNullOrWhiteSpace(Nombre))
            return false;

        return true;
    }
}
