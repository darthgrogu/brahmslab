using BrahmsLab.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BrahmsLab.DataAccess.Data;

public class BrahmsLabDbContext : DbContext
{
    public DbSet<SpectralScan> SpectralScans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = Path.Combine(appDataFolder, "BrahmsLab", "brahmslab.db");

        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Instrui o EF Core a converter os Enums para strings ao salvar no banco.
        modelBuilder.Entity<SpectralScan>()
            .Property(s => s.TargetClass)
            .HasConversion<string>();

        modelBuilder.Entity<SpectralScan>()
            .Property(s => s.BackgroundClass)
            .HasConversion<string>();

        modelBuilder.Entity<SpectralScan>()
            .Property(s => s.TissueDevelopmentalStage)
            .HasConversion<string>();

        modelBuilder.Entity<SpectralScan>()
            .Property(s => s.HasGlue)
            .HasConversion<string>();

        modelBuilder.Entity<SpectralScan>()
            .Property(s => s.HasNonGlueContamination)
            .HasConversion<string>();
    }
}