using Admision.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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

            builder.HasData(
                new Paciente
                {
                    Id = Guid.Parse("11111111-aaaa-aaaa-aaaa-111111111111"), // Coincide con HC 1 en Clínico
                    Nombre = "Carlos Mendoza",
                    Dni = "25444333",
                    FechaNacimiento = new DateTime(1980, 5, 12, 0, 0, 0, DateTimeKind.Utc),
                    Sexo = "Masculino",
                    Telefono = "11-4444-5555"
                },
                new Paciente
                {
                    Id = Guid.Parse("22222222-bbbb-bbbb-bbbb-222222222222"), // Coincide con HC 2 en Clínico
                    Nombre = "Luciana Gómez",
                    Dni = "30111222",
                    FechaNacimiento = new DateTime(1992, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    Sexo = "Femenino",
                    Telefono = "11-2222-3333"
                }
            );
        }
    }
}