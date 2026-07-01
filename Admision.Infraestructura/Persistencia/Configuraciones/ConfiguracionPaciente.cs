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
                    Dni = "21123456",
                    FechaNacimiento = new DateTime(1971, 5, 12, 0, 0, 0, DateTimeKind.Utc),
                    Sexo = "Masculino",
                    Telefono = "11-4444-5555"
                },
                new Paciente
                {
                    Id = Guid.Parse("22222222-bbbb-bbbb-bbbb-222222222222"), // Coincide con HC 2 en Clínico
                    Nombre = "Luciana Gómez",
                    Dni = "22123456",
                    FechaNacimiento = new DateTime(1972, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    Sexo = "Femenino",
                    Telefono = "11-2222-3333"
                },
                new Paciente
                {
                    Id = Guid.Parse("33333333-2373-cccc-cccc-333333333333"),
                    Nombre = "Roberto Sánchez",
                    Dni = "23123456",
                    FechaNacimiento = new DateTime(1973, 2, 10, 0, 0, 0, DateTimeKind.Utc),
                    Sexo = "Masculino",
                    Telefono = "11-9999-8888"
                },
                new Paciente
                {
                    Id = Guid.Parse("44444444-2474-dddd-dddd-444444444444"),
                    Nombre = "María Fernández",
                    Dni = "24123456",
                    FechaNacimiento = new DateTime(1974, 8, 15, 0, 0, 0, DateTimeKind.Utc),
                    Sexo = "Femenino",
                    Telefono = "11-7777-6666"
                },
                new Paciente
                {
                    Id = Guid.Parse("55555555-2575-eeee-eeee-555555555555"),
                    Nombre = "Javier Rodríguez",
                    Dni = "25123456",
                    FechaNacimiento = new DateTime(1975, 11, 20, 0, 0, 0, DateTimeKind.Utc),
                    Sexo = "Masculino",
                    Telefono = "11-5555-4444"
                },
                new Paciente
                {
                    Id = Guid.Parse("66666666-2676-ffff-ffff-666666666666"),
                    Nombre = "Silvana López",
                    Dni = "26123456",
                    FechaNacimiento = new DateTime(1976, 4, 5, 0, 0, 0, DateTimeKind.Utc),
                    Sexo = "Femenino",
                    Telefono = "11-3333-2222"
                },
                new Paciente
                {
                    Id = Guid.Parse("77777777-2777-aaaa-aaaa-777777777777"),
                    Nombre = "Diego Martínez",
                    Dni = "27123456",
                    FechaNacimiento = new DateTime(1977, 1, 30, 0, 0, 0, DateTimeKind.Utc),
                    Sexo = "Masculino",
                    Telefono = "11-1111-0000"
                },
                new Paciente
                {
                    Id = Guid.Parse("88888888-2878-bbbb-bbbb-888888888888"),
                    Nombre = "Valeria Torres",
                    Dni = "28123456",
                    FechaNacimiento = new DateTime(1978, 9, 18, 0, 0, 0, DateTimeKind.Utc),
                    Sexo = "Femenino",
                    Telefono = "11-0000-1111"
                },
                new Paciente
                {
                    Id = Guid.Parse("99999999-2979-cccc-cccc-999999999999"),
                    Nombre = "Gustavo Romero",
                    Dni = "29123456",
                    FechaNacimiento = new DateTime(1979, 7, 7, 0, 0, 0, DateTimeKind.Utc),
                    Sexo = "Masculino",
                    Telefono = "11-2222-1111"
                },
                new Paciente
                {
                    Id = Guid.Parse("10101010-3080-dddd-dddd-101010101010"),
                    Nombre = "Natalia Silva",
                    Dni = "30123456",
                    FechaNacimiento = new DateTime(1980, 12, 12, 0, 0, 0, DateTimeKind.Utc),
                    Sexo = "Femenino",
                    Telefono = "11-3333-5555"
                }
            );
        }
    }
}