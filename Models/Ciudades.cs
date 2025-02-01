using System.ComponentModel.DataAnnotations;

namespace Registrotecnico.Models
{
    public class Ciudades
    {
        [Key]
        public int CiudadId { get; set; }

        public string Nombres { get; set; }
    }
}
