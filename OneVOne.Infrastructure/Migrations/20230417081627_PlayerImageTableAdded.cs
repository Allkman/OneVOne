using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneVOne.GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PlayerImageTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerImages_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerImages_PlayerId",
                table: "PlayerImages",
                column: "PlayerId",
                unique: true,
                filter: "[PlayerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerImages");
        }
    }
}
