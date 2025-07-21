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
}