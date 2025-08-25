// File: src/4_Shells/BrahmsLab.WinUi3App/ViewModels/SpectralScanFormViewModel.cs

using Brahms.Api.Contracts.Enums.iHerbSpec;
using Brahms.Api.Contracts.Helpers;
using BrahmsLab.Core.Interfaces;
using BrahmsLab.Core.Messages;
using BrahmsLab.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class SpectralScanFormViewModel : ObservableObject
{
    private readonly ISpectralScanRepository _spectralScanRepository;

    // --- Listas de Opções para os ComboBoxes ---
    public ObservableCollection<string> HerbariumCodes { get; } = new();
    public ObservableCollection<TargetClass> TargetClasses { get; }
    public ObservableCollection<BackgroundClass> BackgroundClasses { get; }
    public ObservableCollection<DevelopmentalStage> DevelopmentalStages { get; }
    public ObservableCollection<Presence> PresenceOptions { get; }

    // --- DEBUG VISUAL ---
    [ObservableProperty] public partial string? MeasurementFlagsString { get; set; }


    // --- Propriedades de Binding para o Formulário ---

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveScanCommand))]
    public partial string? UserInputCode { get; set; }

    [ObservableProperty] public partial string? SelectedHerbariumCode { get; set; }
    [ObservableProperty] public partial TargetClass? SelectedTargetClass { get; set; }
    [ObservableProperty] public partial BackgroundClass? SelectedBackgroundClass { get; set; }
    [ObservableProperty] public partial DevelopmentalStage? SelectedDevelopmentalStage { get; set; }
    [ObservableProperty] public partial bool HasLowReflectanceBackground { get; set; }
    [ObservableProperty] public partial bool HasBackgroundInMeasurement { get; set; }
    [ObservableProperty] public partial double PercentBackgroundInMeasurement { get; set; }
    [ObservableProperty] public partial Presence? SelectedHasGlue { get; set; }
    [ObservableProperty] public partial Presence? SelectedHasNonGlueContamination { get; set; }

    [ObservableProperty] public partial MeasurementFlags MeasurementFlags { get; set; }
    
    public ObservableCollection<SelectableFlagViewModel> Flags { get; } = new();

    [ObservableProperty] public partial string? TissueNotes { get; set; }
    [ObservableProperty] public partial string? GeneralComment { get; set; }
    [ObservableProperty] public partial string? TissueLocationText { get; set; }

    public SpectralScanFormViewModel(ISpectralScanRepository spectralScanRepository)
    {
        _spectralScanRepository = spectralScanRepository;

        // Converte os Enums em coleções que podem ser usadas pelos ComboBoxes
        TargetClasses = new(Enum.GetValues<TargetClass>());
        BackgroundClasses = new(Enum.GetValues<BackgroundClass>());
        DevelopmentalStages = new(Enum.GetValues<DevelopmentalStage>());
        PresenceOptions = new(Enum.GetValues<Presence>());

        LoadOptions();
        InitializeFlags();
    }

    private void LoadOptions()
    {
        // Pré-carrega códigos de herbário comuns
        HerbariumCodes.Add("INPA");
        HerbariumCodes.Add("GH");
        HerbariumCodes.Add("NY");
    }

    private void InitializeFlags()
    {
        // Usamos Enum.GetValues para obter todas as opções definidas no nosso Enum
        var allFlags = Enum.GetValues<MeasurementFlags>();

        foreach (var flag in allFlags)
        {
            if (flag == MeasurementFlags.None) continue;

            var flagViewModel = new SelectableFlagViewModel(flag);
            flagViewModel.PropertyChanged += OnFlagSelectionChanged;

            Flags.Add(flagViewModel);
        }
    }

    private void OnFlagSelectionChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Verificamos se foi realmente a propriedade "IsSelected" que mudou
        if (e.PropertyName == nameof(SelectableFlagViewModel.IsSelected))
        {
            UpdateMeasurementFlags();
        }
    }

    private void UpdateMeasurementFlags()
    {
        // Começa com "nenhum interruptor ligado"
        var combinedFlags = MeasurementFlags.None;

        // Percorre todos os "micro-cérebros" na lista
        foreach (var flagViewModel in Flags)
        {
            // Se o interruptor estiver selecionado (ligado)...
            if (flagViewModel.IsSelected)
            {
                // ...combina a flag com o valor total usando a operação de bits OR (|)
                combinedFlags |= flagViewModel.Flag;
            }
        }

        MeasurementFlags = combinedFlags;
        MeasurementFlagsString = combinedFlags.ToPipeSeparatedString();
    }

    [SupportedOSPlatform("windows10.0.17763.0")]
    [RelayCommand(CanExecute = nameof(CanSaveScan))]
    private async Task SaveScanAsync()
    {
        var newScan = new LocalSpectralReading
        {
            UserInputCode = this.UserInputCode,
            HerbariumCode = this.SelectedHerbariumCode,
            TargetClass = this.SelectedTargetClass,
            BackgroundClass = this.SelectedBackgroundClass,
            TissueDevelopmentalStage = this.SelectedDevelopmentalStage,
            HasLowReflectanceBackground = this.HasLowReflectanceBackground,
            HasBackgroundInMeasurement = this.HasBackgroundInMeasurement,
            PercentBackgroundInMeasurement = (int?)this.PercentBackgroundInMeasurement,
            HasGlue = this.SelectedHasGlue,
            HasNonGlueContamination = this.SelectedHasNonGlueContamination,
            MeasurementFlags = this.MeasurementFlags,
            TissueNotes = this.TissueNotes,
            GeneralComment = this.GeneralComment,

            // Campos de controlo
            SyncStatus = SyncStatus.New,
            Version = 1,
            UpdatedAtUtc = DateTime.UtcNow,
            SpectrumJsonData = GenerateMockSpectrumData()
        };

        var savedScan = await _spectralScanRepository.AddAsync(newScan);

        WeakReferenceMessenger.Default.Send(new SpectralScanSavedMessage(savedScan));
        ClearForm();
    }

    private bool CanSaveScan()
    {
        // A validação agora pode incluir outros campos obrigatórios
        return !string.IsNullOrWhiteSpace(UserInputCode);
    }

    private void ClearForm()
    {
        UserInputCode = string.Empty;
        SelectedHerbariumCode = HerbariumCodes.FirstOrDefault();
        SelectedTargetClass = null;
        SelectedBackgroundClass = null;
        SelectedDevelopmentalStage = null;
        HasLowReflectanceBackground = false;
        HasBackgroundInMeasurement = false;
        PercentBackgroundInMeasurement = 0;
        SelectedHasGlue = null;
        SelectedHasNonGlueContamination = null;
        MeasurementFlags = MeasurementFlags.None;
        TissueNotes = string.Empty;
        GeneralComment = string.Empty;

        foreach (var flagVm in Flags)
        {
            flagVm.IsSelected = false; // Isto irá acionar o UpdateMeasurementFlags, que limpa a propriedade principal.
        }

        MeasurementFlagsString = string.Empty;
    }

    private string GenerateMockSpectrumData()
    {
        var dataPoints = new List<double[]>();
        var rand = new Random();
        for (int i = 0; i < 200; i++)
        {
            // Gera uma curva senoidal com um pouco de ruído aleatório
            double x = 400 + i * 10; // Simula comprimento de onda de 400 a 2400 nm
            double y = Math.Sin(i * Math.PI / 100) + (rand.NextDouble() - 0.5) * 0.2;
            dataPoints.Add(new[] { x, y });
        }
        return JsonConvert.SerializeObject(dataPoints);
    }
}