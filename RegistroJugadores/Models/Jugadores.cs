using  System.ComponentModel.DataAnnotations;

namespace RegistroJugadores.Models
{
    public class Jugadores
    {
        [Key]
        public int JugadorId { get; set; }
        [Required(ErrorMessage = "El campo JugadoId es obligatorio")]
        public string Nombres { get; set; }

        public int Partidas { get; set; }
    }
}
