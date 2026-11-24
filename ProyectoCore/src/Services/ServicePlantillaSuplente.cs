using ProyectoCore.Repositories.Interfaces;

namespace ProyectoCore.Services;

public class ServicePlantillaSuplente
{
    private readonly IRepoPlantillaSuplente _repo;
    private readonly IRepoPlantilla _repoPlantilla;
    private readonly IRepoJugador _repoJugador;

    public ServicePlantillaSuplente(
        IRepoPlantillaSuplente repo,
        IRepoPlantilla repoPlantilla,
        IRepoJugador repoJugador)
    {
        _repo = repo;
        _repoPlantilla = repoPlantilla;
        _repoJugador = repoJugador;
    }

    private void ValidarIds(int plantillaId, int jugadorId)
    {
        if (plantillaId <= 0)
            throw new ArgumentOutOfRangeException(nameof(plantillaId), "El ID de la plantilla debe ser mayor a 0.");
        if (jugadorId <= 0)
            throw new ArgumentOutOfRangeException(nameof(jugadorId), "El ID del jugador debe ser mayor a 0.");
        if (_repoPlantilla.ObtenerPorId(plantillaId) == null)
            throw new ArgumentException("La plantilla indicada no existe.", nameof(plantillaId));
        if (_repoJugador.ObtenerPorId(jugadorId) == null)
            throw new ArgumentException("El jugador indicado no existe.", nameof(jugadorId));
    }

    //to do : agregar validaciones de existencia de plantilla y jugador antes de agregar o remover titulares
    public bool AgregarSuplente(int plantillaId, int jugadorId)
    {
        ValidarIds(plantillaId, jugadorId);
        return _repo.AgregarSuplente(plantillaId, jugadorId);
    }

    public bool RemoverSuplente(int plantillaId, int jugadorId)
    {
        ValidarIds(plantillaId, jugadorId);
        return _repo.RemoverSuplente(plantillaId, jugadorId);
    }
}
