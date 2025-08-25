using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrahmsLab.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialLocalDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpectralScans");

            migrationBuilder.CreateTable(
                name: "LocalSessions",
                columns: table => new
                {
                    LocalSessionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServerSessionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProtocolSessionId = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<string>(type: "TEXT", nullable: false),
                    InstrumentModel = table.Column<string>(type: "TEXT", nullable: false),
                    OpticalSetupDescription = table.Column<string>(type: "TEXT", nullable: false),
                    MeasurementSettings = table.Column<string>(type: "TEXT", nullable: false),
                    WhiteReferenceDescription = table.Column<string>(type: "TEXT", nullable: false),
                    OperatorName = table.Column<string>(type: "TEXT", nullable: true),
                    LightSourceType = table.Column<string>(type: "TEXT", nullable: true),
                    DistanceTargetToSensor = table.Column<int>(type: "INTEGER", nullable: true),
                    LensFieldOfView = table.Column<int>(type: "INTEGER", nullable: true),
                    AngleLightToSensor = table.Column<int>(type: "INTEGER", nullable: true),
                    MeasurementAreaDiameter = table.Column<int>(type: "INTEGER", nullable: true),
                    SyncStatus = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalSessions", x => x.LocalSessionId);
                });

            migrationBuilder.CreateTable(
                name: "LocalSpectralReadings",
                columns: table => new
                {
                    LocalReadingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServerReadingId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LocalSessionId = table.Column<int>(type: "INTEGER", nullable: false),
                    HerbariumCode = table.Column<string>(type: "TEXT", nullable: false),
                    SpecimenId = table.Column<string>(type: "TEXT", nullable: false),
                    IsTempControlled = table.Column<bool>(type: "INTEGER", nullable: true),
                    AnnualTempMin = table.Column<string>(type: "TEXT", nullable: true),
                    AnnualTempMax = table.Column<string>(type: "TEXT", nullable: true),
                    IsHumidityControlled = table.Column<bool>(type: "INTEGER", nullable: true),
                    AnnualHumidityMin = table.Column<string>(type: "TEXT", nullable: true),
                    AnnualHumidityMax = table.Column<string>(type: "TEXT", nullable: true),
                    BackgroundClass = table.Column<string>(type: "TEXT", nullable: false),
                    HasLowReflectanceBackground = table.Column<bool>(type: "INTEGER", nullable: false),
                    BackgroundDescription = table.Column<string>(type: "TEXT", nullable: true),
                    TargetClass = table.Column<string>(type: "TEXT", nullable: false),
                    TargetTissueId = table.Column<string>(type: "TEXT", nullable: true),
                    TissueDevelopmentalStage = table.Column<string>(type: "TEXT", nullable: false),
                    HasBackgroundInMeasurement = table.Column<bool>(type: "INTEGER", nullable: false),
                    PercentBackgroundInMeasurement = table.Column<int>(type: "INTEGER", nullable: true),
                    HasGlue = table.Column<string>(type: "TEXT", nullable: false),
                    HasNonGlueContamination = table.Column<string>(type: "TEXT", nullable: false),
                    MeasurementFlags = table.Column<int>(type: "INTEGER", nullable: true),
                    TissueNotes = table.Column<string>(type: "TEXT", nullable: true),
                    TissueLocation = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    MeasurementIndex = table.Column<string>(type: "TEXT", nullable: false),
                    SpectralDataJson = table.Column<string>(type: "TEXT", nullable: false),
                    SyncStatus = table.Column<string>(type: "TEXT", nullable: false),
                    SyncErrorMessage = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalSpectralReadings", x => x.LocalReadingId);
                    table.ForeignKey(
                        name: "FK_LocalSpectralReadings_LocalSessions_LocalSessionId",
                        column: x => x.LocalSessionId,
                        principalTable: "LocalSessions",
                        principalColumn: "LocalSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalSpectralReadings_LocalSessionId",
                table: "LocalSpectralReadings",
                column: "LocalSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalSpectralReadings");

            migrationBuilder.DropTable(
                name: "LocalSessions");

            migrationBuilder.CreateTable(
                name: "SpectralScans",
                columns: table => new
                {
                    LocalId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BackgroundClass = table.Column<string>(type: "TEXT", nullable: true),
                    BackgroundDescription = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralComment = table.Column<string>(type: "TEXT", nullable: true),
                    HasBackgroundInMeasurement = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasGlue = table.Column<string>(type: "TEXT", nullable: true),
                    HasLowReflectanceBackground = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasNonGlueContamination = table.Column<string>(type: "TEXT", nullable: true),
                    HerbariumCode = table.Column<string>(type: "TEXT", nullable: true),
                    MeasurementFlags = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasurementIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    Operator = table.Column<string>(type: "TEXT", nullable: true),
                    PercentBackgroundInMeasurement = table.Column<int>(type: "INTEGER", nullable: true),
                    ProjectId = table.Column<string>(type: "TEXT", nullable: true),
                    SessionId = table.Column<string>(type: "TEXT", nullable: true),
                    SpectrumJsonData = table.Column<string>(type: "TEXT", nullable: true),
                    SyncStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetClass = table.Column<string>(type: "TEXT", nullable: true),
                    TargetTissueId = table.Column<string>(type: "TEXT", nullable: true),
                    TissueDevelopmentalStage = table.Column<string>(type: "TEXT", nullable: true),
                    TissueLocationX = table.Column<int>(type: "INTEGER", nullable: true),
                    TissueLocationY = table.Column<int>(type: "INTEGER", nullable: true),
                    TissueNotes = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserInputCode = table.Column<string>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpectralScans", x => x.LocalId);
                });
        }
    }
}
