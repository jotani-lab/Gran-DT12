namespace ProyectoCore.Models;

public class Plantilla
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Nombre { get; set; } = "Mi Plantilla";
    public decimal PresupuestoMaximo { get; set; } = 100000000.00m;
    public int CantidadMaximaJugadores { get; set; } = 20;

    public List<PlantillaTitular> Titulares { get; set; } = new();
    public List<PlantillaSuplente> Suplentes { get; set; } = new();

    public decimal PresupuestoConsumido()
    {
        decimal total = 0m;
        foreach (var t in Titulares) if (t.Jugador != null) total += t.Jugador.Cotizacion;
        foreach (var s in Suplentes) if (s.Jugador != null) total += s.Jugador.Cotizacion;
        return total;
    }

    public bool EsPresupuestoValido() => PresupuestoConsumido() <= PresupuestoMaximo;

    public bool EsCantidadJugadoresValida() => (Titulares.Count + Suplentes.Count) <= CantidadMaximaJugadores;

    public bool EsValidaFormacionTitular()
    {
        if (Titulares.Count != 11) return false;

        int arqueros = Titulares.Count(t => t.Jugador != null && t.Jugador.PosicionId == 1);
        int defensores = Titulares.Count(t => t.Jugador != null && t.Jugador.PosicionId == 2);
        int mediocampistas = Titulares.Count(t => t.Jugador != null && t.Jugador.PosicionId == 3);
        int delanteros = Titulares.Count(t => t.Jugador != null && t.Jugador.PosicionId == 4);

        return arqueros == 1 && defensores == 4 && mediocampistas == 4 && delanteros == 2;
    }

    public bool EsValida() => EsPresupuestoValido() && EsCantidadJugadoresValida() && EsValidaFormacionTitular();

    public decimal PuntajeFecha(int nroFecha)
    {
        if (nroFecha <= 0 || nroFecha >= 50)
            throw new ArgumentOutOfRangeException(nameof(nroFecha), "Número de fecha inválido.");

        decimal total = 0m;
        foreach (var t in Titulares)
        {
            if (t.Jugador != null)
            {
                var p = t.Jugador.Puntuaciones.FirstOrDefault(x => x.NroFecha == nroFecha);
                if (p != null) total += p.Nota;
            }
        }
        return total;
    }

    public bool Verificar()
    {
        if (UsuarioId <= 0)
            return false;

        if (string.IsNullOrWhiteSpace(Nombre))
            return false;

        if (PresupuestoMaximo < 0)
            return false;

        if (CantidadMaximaJugadores <= 0)
            return false;

        return true;
    }
}
