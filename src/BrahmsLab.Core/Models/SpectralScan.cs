using System.ComponentModel.DataAnnotations;

namespace BrahmsLab.Core.Models;

public enum SyncStatus
{
    New,      // Created locally, never synced
    Synced,   // In sync with the cloud
    Modified, // Modified locally, needs to be pushed
    Conflict  // A sync conflict occurred
}

public class SpectralScan
{
    [Key]
    public int LocalId { get; set; }

    public Guid? CloudId { get; set; } // The ID from the cloud, nullable
    public int Version { get; set; }
    public SyncStatus SyncStatus { get; set; }

    // --- Actual Data Fields ---
    public string? ExsicataIdentifier { get; set; }
    public string? SpeciesName { get; set; }
    // We will add more fields like TissueType, etc., here later.

    public DateTime UpdatedAtUtc { get; set; }
}