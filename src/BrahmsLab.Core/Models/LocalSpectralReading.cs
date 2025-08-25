using Brahms.Api.Contracts.Enums.iHerbSpec;
using BrahmsLab.Core.Enums; // Adicione esta linha
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrahmsLab.Core.Models;

public class LocalSpectralReading
{
    [Key]
    public int LocalReadingId { get; set; }
    public Guid? ServerReadingId { get; set; }

    [Required]
    public int LocalSessionId { get; set; }
    [ForeignKey("LocalSessionId")]
    public LocalSession Session { get; set; } = null!;

    // --- Metadados do Espécime (Tabela 4.2) ---
    [Required]
    public string HerbariumCode { get; set; } = string.Empty;
    [Required]
    public string SpecimenId { get; set; } = string.Empty;
    public bool? IsTempControlled { get; set; }
    public string? AnnualTempMin { get; set; }
    public string? AnnualTempMax { get; set; }
    public bool? IsHumidityControlled { get; set; }
    public string? AnnualHumidityMin { get; set; }
    public string? AnnualHumidityMax { get; set; }

    // --- Metadados do Tecido (Tabela 4.3) ---
    [Required]
    public BackgroundClass BackgroundClass { get; set; }
    [Required]
    public bool HasLowReflectanceBackground { get; set; }
    public string? BackgroundDescription { get; set; }
    [Required]
    public TargetClass TargetClass { get; set; }
    public string? TargetTissueId { get; set; }
    [Required]
    public DevelopmentalStage TissueDevelopmentalStage { get; set; } // ATUALIZADO
    [Required]
    public bool HasBackgroundInMeasurement { get; set; }
    public int? PercentBackgroundInMeasurement { get; set; }
    [Required]
    public Presence HasGlue { get; set; } // ATUALIZADO (true/false/uncertain)
    [Required]
    public Presence HasNonGlueContamination { get; set; } // ATUALIZADO (true/false/uncertain)
    public MeasurementFlags? MeasurementFlags { get; set; }
    public string? TissueNotes { get; set; }
    public string? TissueLocation { get; set; }
    //Propriedades mágicas para conversao da string TissueLocation em inteiros X e Y
    [NotMapped]
    public int? LocationX
    {
        get
        {
            if (string.IsNullOrWhiteSpace(TissueLocation)) return null;
            var parts = TissueLocation.Trim().Split(',');
            if (parts.Length == 2 && int.TryParse(parts[0], out int x))
            {
                return x;
            }
            return null;
        }
    }

    [NotMapped]
    public int? LocationY
    {
        get
        {
            if (string.IsNullOrWhiteSpace(TissueLocation)) return null;
            var parts = TissueLocation.Trim().Split(',');
            if (parts.Length == 2 && int.TryParse(parts[1], out int y))
            {
                return y;
            }
            return null;
        }
    }

    public string? Comment { get; set; }
    [Required]
    public string MeasurementIndex { get; set; } = string.Empty;    

    // --- Dados Espectrais e de Arquitetura ---
    [Required]
    public string SpectralDataJson { get; set; } = string.Empty;
    [Required]
    public SyncStatus SyncStatus { get; set; } = SyncStatus.New; // ATUALIZADO
    public string? SyncErrorMessage { get; set; }
}