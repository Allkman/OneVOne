using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneVOne.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NbaPlayerPageUrlAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NbaPlayerPageUrl",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NbaPlayerPageUrl",
                table: "Players");
        }
    }
}
