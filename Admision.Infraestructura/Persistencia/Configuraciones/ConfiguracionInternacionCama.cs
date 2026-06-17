using Admision.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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

            builder.Property(ic => ic.MotivoTraslado)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.HasOne(ic => ic.Internacion)
                .WithMany(i => i.HistorialCamas)
                .HasForeignKey(ic => ic.InternacionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ic => ic.Cama)
                .WithMany(c => c.HistorialInternaciones)
                .HasForeignKey(ic => ic.CamaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new InternacionCama
                {
                    Id = Guid.Parse("88888888-2222-2222-2222-888888888888"),
                    InternacionId = Guid.Parse("66666666-ffff-ffff-ffff-666666666666"), // Internación Carlos
                    CamaId = Guid.Parse("33333333-cccc-cccc-cccc-333333333333"), // Cama 101 UTI
                    FechaIngresoCama = new DateTime(2026, 6, 15, 8, 30, 0, DateTimeKind.Utc),
                    EsActual = true
                },
                new InternacionCama
                {
                    Id = Guid.Parse("99999999-3333-3333-3333-999999999999"),
                    InternacionId = Guid.Parse("77777777-1111-1111-1111-777777777777"), // Internación Luciana
                    CamaId = Guid.Parse("44444444-dddd-dddd-dddd-444444444444"), // Cama 201 Guardia
                    FechaIngresoCama = new DateTime(2026, 6, 16, 10, 30, 0, DateTimeKind.Utc),
                    EsActual = true
                }
            );
        }
    }
}