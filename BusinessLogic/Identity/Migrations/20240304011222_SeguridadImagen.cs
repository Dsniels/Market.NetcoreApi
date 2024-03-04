using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogic.Identity.Migrations
{
    /// <inheritdoc />
    public partial class SeguridadImagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "AspNetUsers");
        }
    }
}
