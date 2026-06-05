using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Admision.Dominio.Entidades;

namespace Admision.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionInternacionCama : IEntityTypeConfiguration<InternacionCama>
    {
        public void Configure(EntityTypeBuilder<InternacionCama> builder)
        {
            builder.ToTable("InternacionesCamas");
            builder.HasKey(ic => ic.Id);

            builder.Property(ic => ic.FechaIngresoCama)
                .IsRequired();

            builder.Property(ic => ic.FechaSalidaCama)
                .IsRequired(false);

            builder.Property(ic => ic.EsActual)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(ic => ic.Internacion)
                .WithMany(i => i.HistorialCamas)
                .HasForeignKey(ic => ic.InternacionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ic => ic.Cama)
                .WithMany(c => c.HistorialInternaciones)
                .HasForeignKey(ic => ic.CamaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}