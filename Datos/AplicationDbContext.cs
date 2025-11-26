using Microsoft.EntityFrameworkCore;
using PropiedadesMinimalApi.Modelo;

namespace ApiPeliculas.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
            
        }

        //Aqui pasar todas la entidades (Modelos)
        public DbSet<Propiedad> Propiedad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Propiedad>().HasData(
            new Propiedad { IdPropiedad = 1, Nombre = "casa 1", Descripcion = "casa desc 1", Ubicacion = "mexico", Activa = true, FechaCreacion = DateTime.Now.AddDays(-10) },
            new Propiedad { IdPropiedad = 2, Nombre = "casa 2", Descripcion = "casa desc 2", Ubicacion = "mexico", Activa = true, FechaCreacion = DateTime.Now.AddDays(-10) },
            new Propiedad { IdPropiedad = 3, Nombre = "casa 3", Descripcion = "casa desc 3", Ubicacion = "mexico", Activa = true, FechaCreacion = DateTime.Now.AddDays(-10) },
            new Propiedad { IdPropiedad = 4, Nombre = "casa 4", Descripcion = "casa desc 4", Ubicacion = "mexico", Activa = true, FechaCreacion = DateTime.Now.AddDays(-10) }
            );
        }


       

    }
}
