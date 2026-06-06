using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Admision.Dominio.Entidades;

namespace Admision.Infraestructura.Persistencia
{
    public class ContextoBaseDeDatos : DbContext
    {
        public ContextoBaseDeDatos(DbContextOptions<ContextoBaseDeDatos> opciones) : base(opciones)
        {
        }
        public DbSet<Sector> Sectores { get; set; }
        public DbSet<Cama> Camas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Internacion> Internaciones { get; set; }
        public DbSet<InternacionCama> InternacionesCamas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}