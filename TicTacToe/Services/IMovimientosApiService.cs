using TicTacToe.Dtos;

namespace TicTacToe.Services;

public interface IMovimientosApiService
{
    Task<Resource<List<MovimientoResponse>>> GetMovimientoAsync(int partidaId);
    Task<Resource<bool>> PostMovimiento(int PartidaId, string Jugador, int PosicionFila, int PosicionColumna);
}