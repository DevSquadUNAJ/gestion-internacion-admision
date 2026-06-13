using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admision.Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class InicializacionAdmision : Migration
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
                    Estado = table.Column<int>(type: "int", maxLength: 50, nullable: false)
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
                    Estado = table.Column<int>(type: "int", maxLength: 50, nullable: false)
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
                    EsActual = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
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
