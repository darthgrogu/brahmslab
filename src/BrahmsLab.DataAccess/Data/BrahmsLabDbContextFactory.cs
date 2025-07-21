using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BrahmsLab.DataAccess.Data;

/// <summary>
/// This factory is used by the EF Core command-line tools (like Add-Migration)
/// to create a DbContext instance at design time. It bypasses the need to
/// run the main UI application's dependency injection setup, which avoids
/// the "Class not registered" error in WinUI 3.
/// </summary>
public class BrahmsLabDbContextFactory : IDesignTimeDbContextFactory<BrahmsLabDbContext>
{
    public BrahmsLabDbContext CreateDbContext(string[] args)
    {
        // This is a simple way to create the DbContext. It will find the
        // connection string inside the OnConfiguring method of our DbContext.
        return new BrahmsLabDbContext();
    }
}