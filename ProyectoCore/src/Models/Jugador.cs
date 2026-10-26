namespace ProyectoCore.Models;

public class Jugador
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string? Apodo { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int EquipoId { get; set; }
    public Equipo? Equipo { get; set; }
    public int PosicionId { get; set; }
    public Posicion? Posicion { get; set; }

    private decimal _cotizacion;
    public decimal Cotizacion
    {
        get => _cotizacion;
        set
        {
            if (value < 0 || value > 99999999.99m)
                throw new ArgumentOutOfRangeException(nameof(value), "La cotización no puede superar $99.999.999,99 ni ser negativa.");
            _cotizacion = value;
        }
    }

    public List<Puntuacion> Puntuaciones { get; set; } = new();

    public void AgregarPuntuacion(Puntuacion puntuacion)
    {
        if (Puntuaciones.Any(p => p.NroFecha == puntuacion.NroFecha))
            throw new InvalidOperationException($"El jugador ya tiene puntuación registrada para la fecha {puntuacion.NroFecha}.");
        Puntuaciones.Add(puntuacion);
    }

    public bool Verificar()
    {
        if (string.IsNullOrWhiteSpace(Nombre))
            return false;

        if (string.IsNullOrWhiteSpace(Apellido))
            return false;

        if (EquipoId <= 0)
            return false;

        if (PosicionId <= 0)
            return false;

        if (Cotizacion < 0 || Cotizacion > 99999999.99m)
            return false;

        return true;
    }
}
