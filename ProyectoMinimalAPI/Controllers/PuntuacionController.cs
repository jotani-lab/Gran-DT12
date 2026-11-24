using Microsoft.AspNetCore.Mvc;
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
       try
       {
           bool ok = _service.RegistrarPuntuacion(puntuacion);
           return ok
               ? Ok(new { message = "Puntuación registrada con éxito." })
               : BadRequest(new { error = "No se pudo registrar la puntuación." });
       }
       catch (Exception ex)
       {
           return BadRequest(new { error = ex.Message });
       }
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


           bool ok = _service.RegistrarPuntuacion(puntuacion);
           return ok
               ? Ok(new { message = "Puntuación registrada con éxito.", puntuacion })
               : BadRequest(new { error = "No se pudo registrar la puntuación." });
       }
       catch (Exception ex)
       {
           return BadRequest(new { error = ex.Message });
       }
   }
}



