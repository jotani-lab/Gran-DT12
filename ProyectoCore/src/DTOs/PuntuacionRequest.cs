namespace ProyectoCore.DTOs;

public class PuntuacionRequest
{
    public int JugadorId { get; set; }
    public int NroMes { get; set; }
    public int NroFecha { get; set; }
    public decimal Nota { get; set; }
}
