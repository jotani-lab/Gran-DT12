using System;
using System.Linq;
using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServicePlantilla
{
    private readonly IRepoPlantilla _repo;
    public ServicePlantilla(IRepoPlantilla repo) { _repo = repo; }

    public List<string> Validar(Plantilla plantilla)
    {
        var errores = new List<string>();

        if (plantilla is null)
        {
            errores.Add("Se debe ingresar una plantilla.");
            return errores;
        }

        if (plantilla.UsuarioId <= 0)
            errores.Add("El usuario no es válido.");

        if (string.IsNullOrWhiteSpace(plantilla.Nombre))
            errores.Add("El nombre de la plantilla es obligatorio.");

        if (plantilla.PresupuestoMaximo <= 0)
            errores.Add("El presupuesto debe ser mayor que cero.");
        else if (plantilla.PresupuestoMaximo > 100000000.00m)
            errores.Add("El presupuesto no puede superar $100.000.000.");

        if (plantilla.CantidadMaximaJugadores != 20)
            errores.Add("La cantidad máxima de jugadores debe ser 20.");

        return errores;
    }

    public List<PlantillaTitular> Titulares { get; set; } = new();
    public List<PlantillaSuplente> Suplentes { get; set; } = new();

    public bool EsValidaFormacionTitular()
    {
        if (Titulares.Count != 11) return false;

        int arqueros = Titulares.Count(t => t.Jugador != null && t.Jugador.PosicionId == 1);
        int defensores = Titulares.Count(t => t.Jugador != null && t.Jugador.PosicionId == 2);
        int mediocampistas = Titulares.Count(t => t.Jugador != null && t.Jugador.PosicionId == 3);
        int delanteros = Titulares.Count(t => t.Jugador != null && t.Jugador.PosicionId == 4);

        return arqueros == 1 && defensores == 4 && mediocampistas == 4 && delanteros == 2;
    }

    public decimal PresupuestoConsumido()
    {
        decimal total = 0m;
        foreach (var t in Titulares) if (t.Jugador != null) total += t.Jugador.Cotizacion;
        foreach (var s in Suplentes) if (s.Jugador != null) total += s.Jugador.Cotizacion;
        return total;
    }

    public bool EsPresupuestoValido(decimal presupuestoMaximo) => PresupuestoConsumido() <= presupuestoMaximo;
    public bool EsCantidadJugadoresValida(int cantidadMaximaJugadores) => (Titulares.Count + Suplentes.Count) <= cantidadMaximaJugadores;
    public decimal PresupuestoRestante(decimal presupuestoMaximo) => presupuestoMaximo - PresupuestoConsumido();

    public decimal PuntajeFecha(Plantilla plantilla, int nroFecha)
    {
        if (plantilla is null)
            throw new ArgumentNullException(nameof(plantilla));

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

    public Plantilla? ObtenerPorUsuarioId(int usuarioId) => _repo.ObtenerPorUsuarioId(usuarioId);
    public Plantilla? ObtenerPorId(int id) => _repo.ObtenerPorId(id);

    public int Crear(Plantilla plantilla)
    {
        var errores = Validar(plantilla);
        if (errores.Count > 0)
            throw new ArgumentException(string.Join(" ", errores), nameof(plantilla));

        return _repo.Crear(plantilla);
    }
}