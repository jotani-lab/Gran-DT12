using ProyectoCore.Models;
using ProyectoCore.Repositories.Interfaces;


namespace ProyectoCore.Services;


public class ServicePuntuacion
{
   private readonly IRepoPuntuacion _repo;
   public ServicePuntuacion(IRepoPuntuacion repo) { _repo = repo; }

   private static void Validar(Puntuacion puntuacion)
   {
       ArgumentNullException.ThrowIfNull(puntuacion);

       if (puntuacion.JugadorId <= 0)
           throw new ArgumentOutOfRangeException(nameof(puntuacion.JugadorId), "El ID del jugador debe ser mayor a 0.");

       if (puntuacion.NroMes <= 0 || puntuacion.NroMes > 12)
           throw new ArgumentOutOfRangeException(nameof(puntuacion.NroMes), "El número de mes debe estar entre 1 y 12.");

       if (puntuacion.NroFecha <= 0 || puntuacion.NroFecha >= 50)
           throw new ArgumentOutOfRangeException(nameof(puntuacion.NroFecha), "El número de fecha debe ser menor a 50.");


       if (puntuacion.Nota < 1.0m || puntuacion.Nota > 10.0m)
           throw new ArgumentOutOfRangeException(nameof(puntuacion.Nota), "La nota debe ser decimal entre 1.0 y 10.0.");
   }

   public bool RegistrarPuntuacion(Puntuacion puntuacion)
   {
       Validar(puntuacion);
       return _repo.RegistrarPuntuacion(puntuacion);
   }

   public bool ActualizarPuntuacion(Puntuacion puntuacion)
   {
       Validar(puntuacion);
       if (puntuacion.IdPuntuacion <= 0)
           throw new ArgumentOutOfRangeException(nameof(puntuacion.IdPuntuacion), "El ID de la puntuación debe ser mayor a 0.");
       return _repo.ActualizarPuntuacion(puntuacion);
   }

   public bool BorrarPorId(int idPuntuacion)
   {
       if (idPuntuacion <= 0)
           throw new ArgumentOutOfRangeException(nameof(idPuntuacion), "El ID de la puntuación debe ser mayor a 0.");
       return _repo.BorrarPorId(idPuntuacion);
   }

   public Puntuacion? ObtenerPorId(int idPuntuacion)
   {
       if (idPuntuacion <= 0)
           throw new ArgumentOutOfRangeException(nameof(idPuntuacion), "El ID de la puntuación debe ser mayor a 0.");
       return _repo.ObtenerPorId(idPuntuacion);
   }

   public List<Puntuacion> ObtenerTodas() => _repo.ObtenerTodas();

   public List<Puntuacion> ObtenerPorJugadorId(int jugadorId)
   {
       if (jugadorId <= 0)
           throw new ArgumentOutOfRangeException(nameof(jugadorId), "El ID del jugador debe ser mayor a 0.");
       return _repo.ObtenerPorJugadorId(jugadorId);
   }

   public List<Puntuacion> ObtenerPorMes(int nroMes)
   {
       if (nroMes <= 0 || nroMes > 12)
           throw new ArgumentOutOfRangeException(nameof(nroMes), "El número de mes debe estar entre 1 y 12.");
       return _repo.ObtenerPorMes(nroMes);
   }

   public Puntuacion? ObtenerPorJugadorMesFecha(int jugadorId, int nroMes, int nroFecha)
   {
       if (jugadorId <= 0)
           throw new ArgumentOutOfRangeException(nameof(jugadorId), "El ID del jugador debe ser mayor a 0.");
       if (nroMes <= 0 || nroMes > 12)
           throw new ArgumentOutOfRangeException(nameof(nroMes), "El número de mes debe estar entre 1 y 12.");
       if (nroFecha <= 0 || nroFecha >= 50)
           throw new ArgumentOutOfRangeException(nameof(nroFecha), "El número de fecha debe ser menor a 50.");
       return _repo.ObtenerPorJugadorMesFecha(jugadorId, nroMes, nroFecha);
   }
}



