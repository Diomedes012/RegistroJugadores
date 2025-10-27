using System.Net.Http.Json;
using TicTacToe.Dtos;

namespace TicTacToe.Services;

public class MovimientosApiService(HttpClient httpClient) : IMovimientosApiService
{
    public async Task<Resource<List<MovimientoResponse>>> GetMovimientoAsync(int partidaId)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<List<MovimientoResponse>>($"api/Movimientos/{partidaId}");
            return new Resource<List<MovimientoResponse>>.Success(response ?? []);
        }
        catch (Exception ex)
        {
            return new Resource<List<MovimientoResponse>>.Error(ex.Message);
        }
    }

    public async Task<Resource<bool>> PostMovimiento(int PartidaId, string Jugador, int PosicionFila, int PosicionColumna)
    {
        var request = new MovimientoRequest(PartidaId, Jugador, PosicionFila, PosicionColumna);
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/Movimientos", request);
            response.EnsureSuccessStatusCode();

            return new Resource<bool>.Success(true);
        }
        catch (HttpRequestException ex)
        {
            return new Resource<bool>.Error($"Error de red: {ex.Message}");
        }
        catch (NotSupportedException)
        {
            return new Resource<bool>.Error("Respuesta inválida del servidor.");
        }

        catch (Exception ex)
        {
            return new Resource<bool>.Error(ex.Message);
        }
    }
}