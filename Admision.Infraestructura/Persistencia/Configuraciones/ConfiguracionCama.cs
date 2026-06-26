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
                // ==========================================
                // SECTOR: UTI (99999999-9999-9999-9999-999999999999)
                // ==========================================
                new Cama { Id = Guid.Parse("33333333-cccc-cccc-cccc-333333333333"), SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Numero = 201, Estado = EstadoCama.Ocupada }, // ORIGINAL
                new Cama { Id = Guid.Parse("55555555-eeee-eeee-eeee-555555555555"), SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Numero = 202, Estado = EstadoCama.Disponible }, // ORIGINAL
                new Cama { Id = Guid.Parse("33333333-0203-cccc-cccc-333333333333"), SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Numero = 203, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("33333333-0204-cccc-cccc-333333333333"), SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Numero = 204, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("33333333-0205-cccc-cccc-333333333333"), SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Numero = 205, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("33333333-0206-cccc-cccc-333333333333"), SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Numero = 206, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("33333333-0207-cccc-cccc-333333333333"), SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Numero = 207, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("33333333-0208-cccc-cccc-333333333333"), SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Numero = 208, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("33333333-0209-cccc-cccc-333333333333"), SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Numero = 209, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("33333333-0210-cccc-cccc-333333333333"), SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Numero = 210, Estado = EstadoCama.Disponible },

                // ==========================================
                // SECTOR: GUARDIA (88888888-8888-8888-8888-888888888888)
                // ==========================================
                new Cama { Id = Guid.Parse("44444444-dddd-dddd-dddd-444444444444"), SectorId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Numero = 101, Estado = EstadoCama.Ocupada }, // ORIGINAL
                new Cama { Id = Guid.Parse("66666666-aaaa-aaaa-aaaa-666666666666"), SectorId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Numero = 102, Estado = EstadoCama.Disponible }, // ORIGINAL
                new Cama { Id = Guid.Parse("44444444-0103-dddd-dddd-444444444444"), SectorId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Numero = 103, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("44444444-0104-dddd-dddd-444444444444"), SectorId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Numero = 104, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("44444444-0105-dddd-dddd-444444444444"), SectorId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Numero = 105, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("44444444-0106-dddd-dddd-444444444444"), SectorId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Numero = 106, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("44444444-0107-dddd-dddd-444444444444"), SectorId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Numero = 107, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("44444444-0108-dddd-dddd-444444444444"), SectorId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Numero = 108, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("44444444-0109-dddd-dddd-444444444444"), SectorId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Numero = 109, Estado = EstadoCama.Disponible },
                new Cama { Id = Guid.Parse("44444444-0110-dddd-dddd-444444444444"), SectorId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Numero = 110, Estado = EstadoCama.Disponible }
            );
        }
    }
}