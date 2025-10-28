namespace RegistroJugadores.DTOs;

public class MovimientoResponse
{
    public int MovimientoId { get; set; }

    public string Jugador { get; set; }

    public int PosicionFila { get; set; }

    public int PosicionColumna { get; set; }   
}
