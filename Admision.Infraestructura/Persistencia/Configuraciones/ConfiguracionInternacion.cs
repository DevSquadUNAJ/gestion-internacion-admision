using Admision.Dominio.Constantes;
using Admision.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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

            builder.HasData(
                new Internacion
                {
                    Id = Guid.Parse("66666666-ffff-ffff-ffff-666666666666"),
                    PacienteId = Guid.Parse("11111111-aaaa-aaaa-aaaa-111111111111"), // Carlos
                    FechaIngreso = new DateTime(2026, 6, 15, 8, 0, 0, DateTimeKind.Utc),
                    Motivo = "Ingreso por guardia con cuadro respiratorio agudo.",
                    Estado = EstadoInternacion.Activa
                },
                new Internacion
                {
                    Id = Guid.Parse("77777777-1111-1111-1111-777777777777"),
                    PacienteId = Guid.Parse("22222222-bbbb-bbbb-bbbb-222222222222"), // Luciana
                    FechaIngreso = new DateTime(2026, 6, 16, 10, 0, 0, DateTimeKind.Utc),
                    Motivo = "Control evolutivo post-cirugía.",
                    Estado = EstadoInternacion.Activa
                }
            );
        }
    }
}