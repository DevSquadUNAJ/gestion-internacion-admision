using Admision.Dominio.Constantes;
using Admision.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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

            builder.HasData(
                new Cama
                {
                    Id = Guid.Parse("33333333-cccc-cccc-cccc-333333333333"),
                    SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // UTI
                    Numero = 101,
                    Estado = EstadoCama.Ocupada
                },
                new Cama
                {
                    Id = Guid.Parse("44444444-dddd-dddd-dddd-444444444444"),
                    SectorId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // Guardia
                    Numero = 201,
                    Estado = EstadoCama.Ocupada
                },
                new Cama
                {
                    Id = Guid.Parse("55555555-eeee-eeee-eeee-555555555555"),
                    SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // UTI
                    Numero = 102,
                    Estado = EstadoCama.Disponible
                }
            );
        }
    }
}