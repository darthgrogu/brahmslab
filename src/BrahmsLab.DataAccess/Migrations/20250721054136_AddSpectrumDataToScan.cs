using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrahmsLab.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSpectrumDataToScan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpectrumJsonData",
                table: "SpectralScans",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpectrumJsonData",
                table: "SpectralScans");
        }
    }
}
