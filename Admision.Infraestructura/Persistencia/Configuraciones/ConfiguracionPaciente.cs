using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Admision.Dominio.Entidades;

namespace Admision.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionPaciente : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Pacientes");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Dni)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(p => p.Dni).IsUnique();

            builder.Property(p => p.Sexo)
                .HasMaxLength(20);

            builder.Property(p => p.Telefono)
                .HasMaxLength(30);
        }
    }
}