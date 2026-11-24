namespace ProyectoCore.Models;

public class Puntuacion
{
    public int IdPuntuacion { get; set; }

    public int Id
    {
        get => IdPuntuacion;
        set => IdPuntuacion = value;
    }

    public int JugadorId { get; set; }
    public string NombreJugador { get; set; } = string.Empty;

    private int _nroMes;
    public int NroMes
    {
        get => _nroMes;
        set
        {
            if (value <= 0 || value > 12)
                throw new ArgumentOutOfRangeException(nameof(value), "El número de mes debe estar entre 1 y 12.");
            _nroMes = value;
        }
    }

    private int _nroFecha;
    public int NroFecha
    {
        get => _nroFecha;
        set
        {
            if (value <= 0 || value >= 50)
                throw new ArgumentOutOfRangeException(nameof(value), "El número de fecha debe ser entre 1 y 49.");
            _nroFecha = value;
        }
    }

    private decimal _nota;
    public decimal Nota
    {
        get => _nota;
        set
        {
            if (value < 1.0m || value > 10.0m)
                throw new ArgumentOutOfRangeException(nameof(value), "La nota debe estar entre 1.0 y 10.0.");
            _nota = value;
        }
    }

    public bool Verificar()
    {
        if (IdPuntuacion < 0)
            return false;

        if (JugadorId <= 0)
            return false;

        if (NroMes <= 0 || NroMes > 12)
            return false;

        if (NroFecha <= 0 || NroFecha >= 50)
            return false;

        if (Nota < 1.0m || Nota > 10.0m)
            return false;

        return true;
    }
}
