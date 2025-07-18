using BrahmsLab.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BrahmsLab.DataAccess.Data;

public class BrahmsLabDbContext : DbContext
{
    public DbSet<SpectralScan> SpectralScans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // We'll name our local database file brahmslab.db
        optionsBuilder.UseSqlite("Data Source=brahmslab.db");
    }
}