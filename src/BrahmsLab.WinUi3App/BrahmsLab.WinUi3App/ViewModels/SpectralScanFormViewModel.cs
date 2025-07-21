// File: src/4_Shells/BrahmsLab.WinUi3App/ViewModels/SpectralScanFormViewModel.cs

using BrahmsLab.Core.Interfaces;
using BrahmsLab.Core.Models;
using BrahmsLab.Core.Messages;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
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

        var savedScan = await _spectralScanRepository.AddAsync(newScan);

        WeakReferenceMessenger.Default.Send(new SpectralScanSavedMessage(savedScan));

        //Clear the form for the next entry
        ClearForm();
    }

    private bool CanSaveScan()
    {
        // Validation logic: The "Save" button will only be enabled if this returns true.
        return !string.IsNullOrWhiteSpace(ExsicataIdentifier);
    }

    private void ClearForm()
    {
        ExsicataIdentifier = string.Empty;
        SpeciesName = string.Empty;
    }
}