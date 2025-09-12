using RegistroJugadores.DAL;
using RegistroJugadores.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroJugadores.Services;

    public class JugadoresService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Guardar(Jugadores jugador)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            if (!await Existe(jugador.JugadorId))
            {
                contexto.Jugadores.Add(jugador);
            }
            else
            {
                contexto.Jugadores.Update(jugador);
            }

            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Existe(int JugadorId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Jugadores.AnyAsync(j => j.JugadorId == JugadorId);

        }

        public async Task<bool> ExisteNombre(int id, string nombres)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Jugadores
                .AnyAsync(j => j.Nombres.ToLower() == nombres.ToLower() && j.JugadorId != id);
        }

        private async Task<bool> Insertar(Jugadores jugador)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Jugadores.Add(jugador);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Jugadores jugador)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(jugador);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Jugadores?> Buscar(int jugadorId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Jugadores
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.JugadorId == jugadorId);
        }
        
        public async Task<bool> Eliminar(int jugadorId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Jugadores
                .AsNoTracking()
                .Where(j => j.JugadorId == jugadorId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Jugadores>> Listar(Expression<Func<Jugadores, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Jugadores
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }
    }
