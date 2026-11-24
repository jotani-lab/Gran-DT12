using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServicePlantillaTitular
{
    private readonly IRepoPlantillaTitular _repo;
    private readonly IRepoPlantilla _repoPlantilla;
    private readonly IRepoJugador _repoJugador;

    public ServicePlantillaTitular(
        IRepoPlantillaTitular repo,
        IRepoPlantilla repoPlantilla,
        IRepoJugador repoJugador)
    {
        _repo = repo;
        _repoPlantilla = repoPlantilla;
        _repoJugador = repoJugador;
    }

    private List<string> ValidarIds(int plantillaId, int jugadorId)
    {
        var errores = new List<string>();

        if (plantillaId <= 0)
            errores.Add("El ID de la plantilla debe ser mayor a 0.");
        if (jugadorId <= 0)
            errores.Add("El ID del jugador debe ser mayor a 0.");
        if (plantillaId > 0 && _repoPlantilla.ObtenerPorId(plantillaId) == null)
            errores.Add("La plantilla indicada no existe.");
        if (jugadorId > 0 && _repoJugador.ObtenerPorId(jugadorId) == null)
            errores.Add("El jugador indicado no existe.");

        return errores;
    }

    //to do : agregar validaciones de existencia de plantilla y jugador antes de agregar o remover titulares
    public bool AgregarTitular(int plantillaId, int jugadorId)
    {
        var errores = ValidarIds(plantillaId, jugadorId);
        if (errores.Count > 0)
            throw new ArgumentException(string.Join(" ", errores));
        return _repo.AgregarTitular(plantillaId, jugadorId);
    }

    public bool RemoverTitular(int plantillaId, int jugadorId)
    {
        var errores = ValidarIds(plantillaId, jugadorId);
        if (errores.Count > 0)
            throw new ArgumentException(string.Join(" ", errores));
        return _repo.RemoverTitular(plantillaId, jugadorId);
    }
}
