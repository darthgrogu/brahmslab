using BrahmsLab.Core.Interfaces;
using BrahmsLab.Core.Models;
using BrahmsLab.DataAccess.Data;
// Add using statements for specific queries if needed in the future
// using System.Collections.Generic;
// using System.Threading.Tasks;

namespace BrahmsLab.DataAccess.Repositories;

public class SpectralScanRepository : GenericRepository<LocalSpectralReading>, ISpectralScanRepository
{
    public SpectralScanRepository(BrahmsLabDbContext context) : base(context){}
}