using BrahmsLab.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrahmsLab.Core.Interfaces;

public interface ISpectralScanRepository : IGenericRepository<SpectralScan>
{
    // Example of a future specific method:
    // Task<List<SpectralScan>> GetScansBySpeciesAsync(string speciesName);
}