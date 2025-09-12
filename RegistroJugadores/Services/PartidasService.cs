using RegistroJugadores.DAL;
using RegistroJugadores.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroJugadores.Services;

public class PartidasService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Partidas partida)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if(!await Existe(partida.PartidaId))
        {
            contexto.Partidas.Add(partida);
        }
        else
        {
            contexto.Partidas.Update(partida);
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int partidaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Partidas.AnyAsync(partida => partida.PartidaId == partidaId);
    }

    private async Task<bool> Insertar(Partidas partida)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Partidas.Add(partida);
        return await contexto.SaveChangesAsync() > 0;

    }

    private async Task<bool> Modificar(Partidas partida)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(partida);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Partidas?> Buscar(int partidaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Partidas
            .AsNoTracking()
            .FirstOrDefaultAsync(partida => partida.PartidaId == partidaId);
    }

    public async Task<bool> Eliminar(int partidaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Partidas
            .AsNoTracking()
            .Where(partida => partida.PartidaId == partidaId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Partidas>> Listar(Expression<Func<Partidas, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Partidas
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
