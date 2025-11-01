using System.Text.Json.Serialization;

namespace RegistroJugadores.DTOs;

public class MovimientoResponse
{
    [JsonPropertyName("movimientoId")]
    public int MovimientoId { get; set; }

    [JsonPropertyName("jugador")]
    public string Jugador { get; set; }

    [JsonPropertyName("posicionFila")]
    public int PosicionFila { get; set; }

    [JsonPropertyName("posicionColumna")]
    public int PosicionColumna { get; set; }
}
