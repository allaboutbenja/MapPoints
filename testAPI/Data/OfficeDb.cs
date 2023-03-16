using Microsoft.EntityFrameworkCore;
using testAPI.Models;

namespace testAPI.Data
{
    public class OfficeDb : DbContext
    {

        public OfficeDb(DbContextOptions<OfficeDb> options) : base(options)
        //Constructor que recibirá DbContextOptions que usaré para la conexión.
        {
        
        }
        public DbSet<MapPoint> MapPoints => Set<MapPoint>();
        //Representación de la Tabla Empleados.
    }
}
