using Microsoft.EntityFrameworkCore;
using RegistroJugadores.Models;

namespace RegistroJugadores.DAL;
    public class Contexto : DbContext
    {
        public DbSet<Jugadores> Jugadores { get; set; }
        public DbSet<Partidas> Partidas { get; set; }
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jugadores>()
                .HasIndex(j => j.Nombres)
                .IsUnique();
        }
    }
