using System.Text.Json.Serialization;

namespace RegistroJugadores.DTOs;

public class MovimientoRequest
{
    [JsonPropertyName("partidaId")]
    public int PartidaId { get; set; }

    [JsonPropertyName("jugador")]
    public string Jugador { get; set; }

    [JsonPropertyName("posicionFila")]
    public int PosicionFila { get; set; }

    [JsonPropertyName("posicionColumna")]
    public int PosicionColumna { get; set; }
}
