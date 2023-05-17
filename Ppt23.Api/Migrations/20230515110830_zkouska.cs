using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ppt23.Api.Migrations
{
    /// <inheritdoc />
    public partial class zkouska : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vybaveni",
                table: "Vybaveni");

            migrationBuilder.RenameTable(
                name: "Vybaveni",
                newName: "Vybavenis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vybavenis",
                table: "Vybavenis",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "Revizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revizes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Revizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vybavenis",
                table: "Vybavenis");

            migrationBuilder.RenameTable(
                name: "Vybavenis",
                newName: "Vybaveni");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vybaveni",
                table: "Vybaveni",
                column: "Name");
        }
    }
}
