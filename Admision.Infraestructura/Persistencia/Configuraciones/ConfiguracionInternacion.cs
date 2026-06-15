using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Admision.Dominio.Entidades;

namespace Admision.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionInternacion : IEntityTypeConfiguration<Internacion>
    {
        public void Configure(EntityTypeBuilder<Internacion> builder)
        {
            builder.ToTable("Internaciones");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Motivo)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(i => i.Estado)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion<string>();

            builder.Property(i => i.FechaIngreso)
                .IsRequired();

            builder.Property(i => i.FechaEgreso)
                .IsRequired(false);

            builder.HasOne(i => i.Paciente)
                .WithMany(p => p.Internaciones)
                .HasForeignKey(i => i.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}