using BrahmsLab.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BrahmsLab.DataAccess.Data;

public class BrahmsLabDbContext : DbContext
{
    public DbSet<LocalSession> LocalSessions { get; set; }
    public DbSet<LocalSpectralReading> LocalSpectralReadings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = Path.Combine(appDataFolder, "BrahmsLab", "brahmslab_local.db");

        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 3. Ensinamos o EF Core a guardar os nossos Enums como strings legíveis no banco.
        modelBuilder.Entity<LocalSpectralReading>()
            .Property(s => s.TargetClass)
            .HasConversion<string>();

        modelBuilder.Entity<LocalSpectralReading>()
            .Property(s => s.BackgroundClass)
            .HasConversion<string>();

        modelBuilder.Entity<LocalSpectralReading>()
            .Property(s => s.TissueDevelopmentalStage)
            .HasConversion<string>();

        modelBuilder.Entity<LocalSpectralReading>()
            .Property(s => s.HasGlue)
            .HasConversion<string>();

        modelBuilder.Entity<LocalSpectralReading>()
            .Property(s => s.HasNonGlueContamination)
            .HasConversion<string>();

        modelBuilder.Entity<LocalSession>()
            .Property(s => s.SyncStatus)
            .HasConversion<string>();

        modelBuilder.Entity<LocalSpectralReading>()
            .Property(r => r.SyncStatus)
            .HasConversion<string>();
    }
}