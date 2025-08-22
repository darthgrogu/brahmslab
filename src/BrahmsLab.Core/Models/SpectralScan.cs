using System.ComponentModel.DataAnnotations;
using Brahms.Api.Contracts.Enums.iHerbSpec;

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
    // --- Campos de Controle Interno do Cliente ---
    [Key]
    public int LocalId { get; set; }
    public SyncStatus SyncStatus { get; set; }
    public int Version { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public string? SpectrumJsonData { get; set; }

    // --- Campos de Sessão (Definidos uma vez por sessão) ---
    public string? ProjectId { get; set; }
    public string? SessionId { get; set; } // Gerado automaticamente no início da sessão
    public string? Operator { get; set; }
    // Outros campos de sessão (instrumentModel, etc.) podem ser adicionados aqui ou gerenciados separadamente.

    // --- Campos do Espécime (Identificação Principal) ---
    public string? UserInputCode { get; set; } // O código de barras ou accession number digitado.
    public string? HerbariumCode { get; set; }

    // --- Campos do Tecido (Metadados da Medição - O coração do formulário) ---
    public int MeasurementIndex { get; set; } // O número da medição (1, 2, 3...) para este alvo.
    public TargetClass? TargetClass { get; set; }
    public string? TargetTissueId { get; set; } // ID para diferenciar múltiplos tecidos do mesmo espécime (ex: "folha1", "folha2")
    public BackgroundClass? BackgroundClass { get; set; }
    public bool HasLowReflectanceBackground { get; set; }
    public string? BackgroundDescription { get; set; }
    public DevelopmentalStage? TissueDevelopmentalStage { get; set; }
    public bool HasBackgroundInMeasurement { get; set; }
    public int? PercentBackgroundInMeasurement { get; set; }
    public Presence? HasGlue { get; set; }
    public Presence? HasNonGlueContamination { get; set; }
    public MeasurementFlags MeasurementFlags { get; set; } // [Flags] Enum para seleção múltipla
    public string? TissueNotes { get; set; }
    public int? TissueLocationX { get; set; } // Anulável, pois o campo é opcional
    public int? TissueLocationY { get; set; } // Anulável, pois o campo é opcional
    public string? GeneralComment { get; set; }
}