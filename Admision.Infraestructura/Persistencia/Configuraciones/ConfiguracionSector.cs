using Admision.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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

            builder.HasData(
                new Sector
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), // Coincide con Enfermeros en Clínico
                    Nombre = "Terapia Intensiva (UTI)",
                    Piso = 2
                },
                new Sector
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    Nombre = "Guardia Clínica",
                    Piso = 1
                }
            );
        }
    }
}