using Microsoft.EntityFrameworkCore;

namespace practica1.Models
{
    public class EquiposContext :DbContext
    {
        public  EquiposContext(DbContextOptions<EquiposContext> options ):base(options)
        
        { 
        


        }
        public DbSet<equipos> equipos { get; set; }
        public DbSet<estados_equipo> estados_equipo { get; set; }
        public DbSet<marcas> marcas { get; set; }
        public DbSet<tipo_equipo> tipo_equipo { get;set; }
    }
}
