using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrahmsLab.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSpectralScanEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpectralScans",
                columns: table => new
                {
                    LocalId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CloudId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    SyncStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    ExsicataIdentifier = table.Column<string>(type: "TEXT", nullable: true),
                    SpeciesName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpectralScans", x => x.LocalId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpectralScans");
        }
    }
}
