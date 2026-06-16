using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Admision.Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicialAdmision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Dni = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Piso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Internaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEgreso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Motivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Internaciones_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Camas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Camas_Sectores_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InternacionesCamas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CamaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaIngresoCama = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSalidaCama = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EsActual = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    MotivoTraslado = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternacionesCamas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternacionesCamas_Camas_CamaId",
                        column: x => x.CamaId,
                        principalTable: "Camas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternacionesCamas_Internaciones_InternacionId",
                        column: x => x.InternacionId,
                        principalTable: "Internaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Pacientes",
                columns: new[] { "Id", "Dni", "FechaNacimiento", "Nombre", "Sexo", "Telefono" },
                values: new object[,]
                {
                    { new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), "25444333", new DateTime(1980, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Carlos Mendoza", "Masculino", "11-4444-5555" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), "30111222", new DateTime(1992, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Luciana Gómez", "Femenino", "11-2222-3333" }
                });

            migrationBuilder.InsertData(
                table: "Sectores",
                columns: new[] { "Id", "Nombre", "Piso" },
                values: new object[,]
                {
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Guardia Clínica", 1 },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Terapia Intensiva (UTI)", 2 }
                });

            migrationBuilder.InsertData(
                table: "Camas",
                columns: new[] { "Id", "Estado", "Numero", "SectorId" },
                values: new object[,]
                {
                    { new Guid("33333333-cccc-cccc-cccc-333333333333"), "Ocupada", 101, new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("44444444-dddd-dddd-dddd-444444444444"), "Ocupada", 201, new Guid("88888888-8888-8888-8888-888888888888") },
                    { new Guid("55555555-eeee-eeee-eeee-555555555555"), "Disponible", 102, new Guid("99999999-9999-9999-9999-999999999999") }
                });

            migrationBuilder.InsertData(
                table: "Internaciones",
                columns: new[] { "Id", "Estado", "FechaEgreso", "FechaIngreso", "Motivo", "PacienteId" },
                values: new object[,]
                {
                    { new Guid("66666666-ffff-ffff-ffff-666666666666"), "Activa", null, new DateTime(2026, 6, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Ingreso por guardia con cuadro respiratorio agudo.", new Guid("11111111-aaaa-aaaa-aaaa-111111111111") },
                    { new Guid("77777777-1111-1111-1111-777777777777"), "Activa", null, new DateTime(2026, 6, 16, 10, 0, 0, 0, DateTimeKind.Utc), "Control evolutivo post-cirugía.", new Guid("22222222-bbbb-bbbb-bbbb-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "InternacionesCamas",
                columns: new[] { "Id", "CamaId", "EsActual", "FechaIngresoCama", "FechaSalidaCama", "InternacionId", "MotivoTraslado" },
                values: new object[,]
                {
                    { new Guid("88888888-2222-2222-2222-888888888888"), new Guid("33333333-cccc-cccc-cccc-333333333333"), true, new DateTime(2026, 6, 15, 8, 30, 0, 0, DateTimeKind.Utc), null, new Guid("66666666-ffff-ffff-ffff-666666666666"), null },
                    { new Guid("99999999-3333-3333-3333-999999999999"), new Guid("44444444-dddd-dddd-dddd-444444444444"), true, new DateTime(2026, 6, 16, 10, 30, 0, 0, DateTimeKind.Utc), null, new Guid("77777777-1111-1111-1111-777777777777"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Camas_SectorId",
                table: "Camas",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Internaciones_PacienteId",
                table: "Internaciones",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_InternacionesCamas_CamaId",
                table: "InternacionesCamas",
                column: "CamaId");

            migrationBuilder.CreateIndex(
                name: "IX_InternacionesCamas_InternacionId",
                table: "InternacionesCamas",
                column: "InternacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Dni",
                table: "Pacientes",
                column: "Dni",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternacionesCamas");

            migrationBuilder.DropTable(
                name: "Camas");

            migrationBuilder.DropTable(
                name: "Internaciones");

            migrationBuilder.DropTable(
                name: "Sectores");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
