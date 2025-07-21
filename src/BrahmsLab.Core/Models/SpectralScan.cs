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
    public Guid? CloudId { get; set; }
    public int Version { get; set; }
    public SyncStatus SyncStatus { get; set; }
    public string? ExsicataIdentifier { get; set; }
    public string? SpeciesName { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}