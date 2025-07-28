using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrahmsLab.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    SyncStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SpectrumJsonData = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectId = table.Column<string>(type: "TEXT", nullable: true),
                    SessionId = table.Column<string>(type: "TEXT", nullable: true),
                    Operator = table.Column<string>(type: "TEXT", nullable: true),
                    UserInputCode = table.Column<string>(type: "TEXT", nullable: true),
                    HerbariumCode = table.Column<string>(type: "TEXT", nullable: true),
                    MeasurementIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetClass = table.Column<string>(type: "TEXT", nullable: true),
                    TargetTissueId = table.Column<string>(type: "TEXT", nullable: true),
                    BackgroundClass = table.Column<string>(type: "TEXT", nullable: true),
                    HasLowReflectanceBackground = table.Column<bool>(type: "INTEGER", nullable: false),
                    BackgroundDescription = table.Column<string>(type: "TEXT", nullable: true),
                    TissueDevelopmentalStage = table.Column<string>(type: "TEXT", nullable: true),
                    HasBackgroundInMeasurement = table.Column<bool>(type: "INTEGER", nullable: false),
                    PercentBackgroundInMeasurement = table.Column<int>(type: "INTEGER", nullable: true),
                    HasGlue = table.Column<string>(type: "TEXT", nullable: true),
                    HasNonGlueContamination = table.Column<string>(type: "TEXT", nullable: true),
                    MeasurementFlags = table.Column<int>(type: "INTEGER", nullable: false),
                    TissueNotes = table.Column<string>(type: "TEXT", nullable: true),
                    TissueLocationX = table.Column<int>(type: "INTEGER", nullable: true),
                    TissueLocationY = table.Column<int>(type: "INTEGER", nullable: true),
                    GeneralComment = table.Column<string>(type: "TEXT", nullable: true)
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
