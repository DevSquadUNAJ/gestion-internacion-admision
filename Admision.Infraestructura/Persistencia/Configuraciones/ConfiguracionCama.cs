using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Admision.Dominio.Entidades;

namespace Admision.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionCama : IEntityTypeConfiguration<Cama>
    {
        public void Configure(EntityTypeBuilder<Cama> builder)
        {
            builder.ToTable("Camas");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Numero)
                .IsRequired();

            builder.Property(c => c.Estado)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion<string>();

            builder.HasOne(c => c.Sector)
                .WithMany(s => s.Camas)
                .HasForeignKey(c => c.SectorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}