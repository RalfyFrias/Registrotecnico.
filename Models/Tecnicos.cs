using System.ComponentModel.DataAnnotations;

namespace Registrotecnico.Models
{
    public class Tecnicos
    {
        [Key]

        public int TecnicoId { get; set; }

        [Required(ErrorMessage = "El Campo Descripción es obligatorio ")]
        public string? Nombres { get; set; }

        [Required(ErrorMessage = "El Campo Descripción es obligatorio")]
        public decimal? Sueldohora { get; set; }
    }
}
