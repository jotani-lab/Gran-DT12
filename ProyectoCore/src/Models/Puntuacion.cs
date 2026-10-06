namespace ProyectoCore.Models;

public class Puntuacion
{
    public int Id { get; set; }
    public int JugadorId { get; set; }

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
        if (JugadorId <= 0)
            return false;

        if (NroFecha <= 0 || NroFecha >= 50)
            return false;

        if (Nota < 1.0m || Nota > 10.0m)
            return false;

        return true;
    }
}
