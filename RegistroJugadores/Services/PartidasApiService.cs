using RegistroJugadores.DTOs;
using System.Net;
using System.Text.Json;
using System.Text;

public class PartidasApiService
{
    private readonly HttpClient _httpClient;

    public PartidasApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("PartidasApi");
    }
    public async Task<List<MovimientoResponse>?> ObtenerMovimientosAsync(int partidaId)
    {
        try
        {
            var movimientos = await _httpClient.GetFromJsonAsync<List<MovimientoResponse>>($"api/Movimientos/{partidaId}");
            return movimientos ?? new List<MovimientoResponse>();
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine($"Partida no encontrada en la API: {partidaId}");
            return new List<MovimientoResponse>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener movimientos: {ex.Message}");
            return null; 
        }
    }
    public async Task<MovimientoResponse?> EnviarMovimientoAsync(MovimientoRequest movimiento)
    {
        string jsonRequest = JsonSerializer.Serialize(movimiento);
        Console.WriteLine($"[API Service] Enviando POST a 'api/movimientos' con: {jsonRequest}");

        try
        {
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/movimientos", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    Console.WriteLine("[API Service] Éxito (200 OK) pero cuerpo vacío. Creando 'MovimientoResponse' manualmente.");

                    return new MovimientoResponse
                    {
                        MovimientoId = 0,
                        Jugador = movimiento.Jugador,
                        PosicionFila = movimiento.PosicionFila,
                        PosicionColumna = movimiento.PosicionColumna
                    };
                }
                else
                {
                    Console.WriteLine("[API Service] Éxito (200 OK) y se recibió JSON.");
                    return JsonSerializer.Deserialize<MovimientoResponse>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            else
            {
                string errorContenido = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[API Service] ERROR: La API falló con código {response.StatusCode}.");
                Console.WriteLine($"[API Service] Contenido del error: {errorContenido}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[API Service] EXCEPCIÓN al enviar POST: {ex.Message}");
            return null;
        }
    }

    public async Task<PartidasResponse?> ObtenerPartidaAsync(int partidaId)
    {
        try
        {
            var partida = await _httpClient.GetFromJsonAsync<PartidasResponse>($"api/Partidas/{partidaId}");
            return partida;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine($"API no encontró la partida: {partidaId}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener la partida: {ex.Message}");
            return null;
        }
    }
}
