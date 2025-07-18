// File: src/BrahmsLab.DataAccess/Repositories/SpectralScanRepository.cs

using BrahmsLab.Core.Interfaces;
using BrahmsLab.Core.Models;
using BrahmsLab.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BrahmsLab.DataAccess.Repositories;

/// <summary>
/// Implements the repository contract using Entity Framework Core against a SQLite database.
/// </summary>
public class SpectralScanRepository : ISpectralScanRepository
{
    private readonly BrahmsLabDbContext _context;

    // The DbContext is provided via Dependency Injection.
    // This class doesn't know how to create a DbContext, it just asks for one.
    public SpectralScanRepository(BrahmsLabDbContext context)
    {
        _context = context;
    }

    public async Task<SpectralScan> AddAsync(SpectralScan spectralScan)
    {
        _context.SpectralScans.Add(spectralScan);
        await _context.SaveChangesAsync();
        return spectralScan;
    }

    public async Task<List<SpectralScan>> GetAllAsync()
    {
        return await _context.SpectralScans.ToListAsync();
    }

    public async Task<SpectralScan?> GetByIdAsync(int localId)
    {
        return await _context.SpectralScans.FindAsync(localId);
    }

    public async Task UpdateAsync(SpectralScan spectralScan)
    {
        // EF Core's change tracker is smart enough to know which fields were modified.
        _context.Entry(spectralScan).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int localId)
    {
        var scan = await _context.SpectralScans.FindAsync(localId);
        if (scan != null)
        {
            _context.SpectralScans.Remove(scan);
            await _context.SaveChangesAsync();
        }
    }
}