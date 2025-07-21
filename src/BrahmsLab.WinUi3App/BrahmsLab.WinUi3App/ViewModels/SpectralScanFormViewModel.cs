// File: src/4_Shells/BrahmsLab.WinUi3App/ViewModels/SpectralScanFormViewModel.cs

using BrahmsLab.Core.Interfaces;
using BrahmsLab.Core.Messages;
using BrahmsLab.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class SpectralScanFormViewModel : ObservableObject
{
    private readonly ISpectralScanRepository _spectralScanRepository;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveScanCommand))]
    public partial string? ExsicataIdentifier { get; set; }

    [ObservableProperty]
    public partial string? SpeciesName { get; set; }

    public SpectralScanFormViewModel(ISpectralScanRepository spectralScanRepository)
    {
        _spectralScanRepository = spectralScanRepository;
    }

    // --- Commands ---

    [RelayCommand(CanExecute = nameof(CanSaveScan))]
    private async Task SaveScanAsync()
    {
        var newScan = new SpectralScan
        {
            ExsicataIdentifier = this.ExsicataIdentifier,
            SpeciesName = this.SpeciesName,
            SyncStatus = SyncStatus.New,
            Version = 1,
            UpdatedAtUtc = DateTime.UtcNow
        };

        newScan.SpectrumJsonData = GenerateMockSpectrumData();

        var savedScan = await _spectralScanRepository.AddAsync(newScan);

        WeakReferenceMessenger.Default.Send(new SpectralScanSavedMessage(savedScan));

        ClearForm();
    }

    private bool CanSaveScan()
    {
        return !string.IsNullOrWhiteSpace(ExsicataIdentifier);
    }

    private void ClearForm()
    {
        ExsicataIdentifier = string.Empty;
        SpeciesName = string.Empty;
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