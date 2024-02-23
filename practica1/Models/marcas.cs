using System.ComponentModel.DataAnnotations;

namespace practica1.Models
{
    public class marcas
    {
        [Key]
        public int id_marca { get; set; }
        public string nombre_marca { get; set; }
        public string estados { get; set; }

    }
}
