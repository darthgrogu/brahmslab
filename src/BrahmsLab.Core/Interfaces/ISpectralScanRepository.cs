using BrahmsLab.Core.Models;

namespace BrahmsLab.Core.Interfaces;

public interface ISpectralScanRepository
{
    Task<SpectralScan?> GetByIdAsync(int localId);
    Task<List<SpectralScan>> GetAllAsync();
    Task<SpectralScan> AddAsync(SpectralScan spectralScan);
    Task UpdateAsync(SpectralScan spectralScan);
    Task DeleteAsync(int localId);
}