using  System.ComponentModel.DataAnnotations;

namespace RegistroJugadores.Models;
    public class Jugadores
    {
        [Key]
        public int JugadorId { get; set; }
        [Required(ErrorMessage = "El campo JugadoId es obligatorio")]


        
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo Nombres es obligatorio")]

        [Range(0, int.MaxValue, ErrorMessage = "El campo Ganadas no puede ser negativo")]
        public int Victorias { get; set; } = 0;

        public int Derrotas { get; set; } = 0;

        public int Empates { get; set; } = 0;
        
    }

