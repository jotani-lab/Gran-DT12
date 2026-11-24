using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ProyectoCore.DTOs;
using ProyectoCore.Models;
using ProyectoCore.Services;


namespace ProyectoMinimalAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PuntuacionController : ControllerBase
{
   private readonly ServicePuntuacion _service;
   public PuntuacionController(ServicePuntuacion service) { _service = service; }


   [HttpGet("jugador/{jugadorId}")]
   public IActionResult GetByJugador(int jugadorId)
   {
       try
       {
           return Ok(_service.ObtenerPorJugadorId(jugadorId).Select(PuntuacionResponse.DesdePuntuacion));
       }
       catch (ArgumentException ex)
       {
           return BadRequest(new { error = ex.Message });
       }
   }

   [HttpGet]
    public IActionResult GetAll() => Ok(_service.ObtenerTodas().Select(PuntuacionResponse.DesdePuntuacion));

   [HttpGet("{idPuntuacion}")]
   public IActionResult GetById(int idPuntuacion)
   {
       try
       {
           var puntuacion = _service.ObtenerPorId(idPuntuacion);
           return puntuacion == null ? NotFound() : Ok(PuntuacionResponse.DesdePuntuacion(puntuacion));
       }
       catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
   }

   [HttpGet("mes/{nroMes}")]
   public IActionResult GetByMes(int nroMes)
   {
    try { return Ok(_service.ObtenerPorMes(nroMes).Select(PuntuacionResponse.DesdePuntuacion)); }
       catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
   }

   [HttpGet("jugador/{jugadorId}/mes/{nroMes}/fecha/{nroFecha}")]
   public IActionResult GetByJugadorMesFecha(int jugadorId, int nroMes, int nroFecha)
   {
       try
       {
           var puntuacion = _service.ObtenerPorJugadorMesFecha(jugadorId, nroMes, nroFecha);
           return puntuacion == null ? NotFound() : Ok(PuntuacionResponse.DesdePuntuacion(puntuacion));
       }
       catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
   }


   [HttpPost]
    public IActionResult Post([FromBody] PuntuacionRequest request)
   {
       if (request == null)
           return BadRequest(new { error = "Los datos de la puntuación son obligatorios." });

       try
       {
           var puntuacion = new Puntuacion
           {
               JugadorId = request.JugadorId,
               NroMes = request.NroMes,
               NroFecha = request.NroFecha,
               Nota = request.Nota
           };
           return Registrar(puntuacion);
       }
       catch (ArgumentException ex)
       {
           return BadRequest(new { error = ex.Message });
       }
   }


   [HttpPost("jugador/{jugadorId}/mes/{nroMes}/fecha/{nroFecha}")]
    public IActionResult PonerPuntaje(int jugadorId, int nroMes, int nroFecha, [FromBody] PuntuarJugadorRequest request)
   {
       if (request == null)
           return BadRequest(new { error = "Los datos de la puntuación son obligatorios." });

       try
       {
           var puntuacion = new Puntuacion
           {
               JugadorId = jugadorId,
               NroMes = nroMes,
               NroFecha = nroFecha,
               Nota = request.Nota
           };

           return Registrar(puntuacion);
       }
       catch (ArgumentException ex)
       {
           return BadRequest(new { error = ex.Message });
       }
   }

   private IActionResult Registrar(Puntuacion puntuacion)
   {
       try
       {
           bool ok = _service.RegistrarPuntuacion(puntuacion);
           return ok
               ? Ok(new { message = "Puntuación registrada con éxito.", puntuacion = PuntuacionResponse.DesdePuntuacion(puntuacion) })
               : BadRequest(new { error = "No se pudo registrar la puntuación." });
       }
       catch (ArgumentException ex)
       {
           return BadRequest(new { error = ex.Message });
       }
       catch (MySqlException ex) when (ex.Number == 1452)
       {
           return BadRequest(new { error = "No existe el jugador indicado." });
       }
   }

   [HttpPut("{idPuntuacion}")]
    public IActionResult Put(int idPuntuacion, [FromBody] PuntuacionRequest request)
   {
       try
       {
           if (request == null)
               return BadRequest(new { error = "Los datos de la puntuación son obligatorios." });

           var puntuacion = new Puntuacion
           {
               JugadorId = request.JugadorId,
               NroMes = request.NroMes,
               NroFecha = request.NroFecha,
               Nota = request.Nota
           };
           puntuacion.IdPuntuacion = idPuntuacion;
           bool ok = _service.ActualizarPuntuacion(puntuacion);
           return ok ? Ok(new { message = "Puntuación actualizada con éxito.", puntuacion = PuntuacionResponse.DesdePuntuacion(puntuacion) })
                     : NotFound(new { error = "No existe la puntuación indicada." });
       }
       catch (ArgumentException ex)
       {
           return BadRequest(new { error = ex.Message });
       }
       catch (MySqlException ex) when (ex.Number == 1062)
       {
           return Conflict(new { error = "Ya existe una puntuación para ese jugador, mes y fecha." });
       }
       catch (MySqlException ex) when (ex.Number == 1452)
       {
           return BadRequest(new { error = "No existe el jugador indicado." });
       }
   }

   [HttpDelete("{idPuntuacion}")]
   public IActionResult Delete(int idPuntuacion)
   {
       try
       {
           bool ok = _service.BorrarPorId(idPuntuacion);
           return ok ? Ok(new { message = "Puntuación eliminada con éxito." })
                     : NotFound(new { error = "No existe la puntuación indicada." });
       }
       catch (ArgumentException ex)
       {
           return BadRequest(new { error = ex.Message });
       }
   }
}



