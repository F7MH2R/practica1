using Microsoft.EntityFrameworkCore;

namespace practica1.Models
{
    public class EquiposContext :DbContext
    {
        public  EquiposContext(DbContextOptions<EquiposContext> options ):base(options)
        
        { 
        


        }
        public DbSet<equipos> equipos { get; set; }
    }
}
