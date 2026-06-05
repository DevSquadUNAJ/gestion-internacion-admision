using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Admision.Dominio.Entidades;

namespace Admision.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionSector : IEntityTypeConfiguration<Sector>
    {
        public void Configure(EntityTypeBuilder<Sector> builder)
        {
            builder.ToTable("Sectores");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Piso)
                .IsRequired();
        }
    }
}