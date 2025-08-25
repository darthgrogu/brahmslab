using System.ComponentModel.DataAnnotations;
using BrahmsLab.Core.Enums;

namespace BrahmsLab.Core.Models;

public class LocalSession
{
    [Key]
    public int LocalSessionId { get; set; } // Chave primária local

    public Guid? ServerSessionId { get; set; } // Sincronização futura

    // Campos do Protocolo iHerbSpec (Tabela 4.1)
    [Required]
    public string ProtocolSessionId { get; set; } = string.Empty;

    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string InstrumentModel { get; set; } = string.Empty;

    [Required]
    public string OpticalSetupDescription { get; set; } = string.Empty;

    [Required]
    public string MeasurementSettings { get; set; } = string.Empty;

    [Required]
    public string WhiteReferenceDescription { get; set; } = string.Empty;

    public string? OperatorName { get; set; }
    public string? LightSourceType { get; set; }
    public int? DistanceTargetToSensor { get; set; }
    public int? LensFieldOfView { get; set; }
    public int? AngleLightToSensor { get; set; }
    public int? MeasurementAreaDiameter { get; set; }

    // Campos de Arquitetura
    [Required]
    public SyncStatus SyncStatus { get; set; } = SyncStatus.New;

    // Propriedade de Navegação
    public ICollection<LocalSpectralReading> SpectralReadings { get; set; } = new List<LocalSpectralReading>();
}