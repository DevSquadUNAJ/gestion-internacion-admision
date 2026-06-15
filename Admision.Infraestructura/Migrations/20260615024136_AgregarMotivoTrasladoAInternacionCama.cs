using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admision.Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class AgregarMotivoTrasladoAInternacionCama : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotivoTraslado",
                table: "InternacionesCamas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivoTraslado",
                table: "InternacionesCamas");
        }
    }
}
