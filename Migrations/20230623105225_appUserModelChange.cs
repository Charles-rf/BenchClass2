using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenchClass.Migrations
{
    /// <inheritdoc />
    public partial class appUserModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUser",
                table: "Gyms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUser",
                table: "Gyms",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
