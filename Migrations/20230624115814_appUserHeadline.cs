using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenchClass.Migrations
{
    /// <inheritdoc />
    public partial class appUserHeadline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeadlineId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Headline",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headline", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HeadlineId",
                table: "AspNetUsers",
                column: "HeadlineId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Headline_HeadlineId",
                table: "AspNetUsers",
                column: "HeadlineId",
                principalTable: "Headline",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Headline_HeadlineId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Headline");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HeadlineId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HeadlineId",
                table: "AspNetUsers");
        }
    }
}
