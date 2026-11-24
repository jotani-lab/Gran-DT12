using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
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
       if (jugadorId <= 0)
           return BadRequest(new { error = "El ID del jugador debe ser mayor a 0." });


       return Ok(_service.ObtenerPorJugadorId(jugadorId));
   }


   [HttpPost]
   public IActionResult Post([FromBody] Puntuacion puntuacion)
   {
       return Registrar(puntuacion);
   }


   [HttpPost("jugador/{jugadorId}/fecha/{nroFecha}")]
   [HttpPut("jugador/{jugadorId}/fecha/{nroFecha}")]
   public IActionResult PonerPuntaje(int jugadorId, int nroFecha, [FromBody] decimal nota)
   {
       try
       {
           var puntuacion = new Puntuacion
           {
               JugadorId = jugadorId,
               NroFecha = nroFecha,
               Nota = nota
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
               ? Ok(new { message = "Puntuación registrada con éxito.", puntuacion })
               : BadRequest(new { error = "No se pudo registrar la puntuación." });
       }
       catch (ArgumentException ex)
       {
           return BadRequest(new { error = ex.Message });
       }
       catch (MySqlException ex) when (ex.Number == 1062)
       {
           return Conflict(new { error = "Ese jugador ya tiene una puntuación registrada para esa fecha." });
       }
       catch (MySqlException ex) when (ex.Number == 1452)
       {
           return BadRequest(new { error = "No existe el jugador indicado." });
       }
       catch (Exception)
       {
           return StatusCode(500, new { error = "Ocurrió un error interno al registrar la puntuación." });
       }
   }
}



