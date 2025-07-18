// File: src/4_Shells/BrahmsLab.WinUi3App/ViewModels/SpectralScanFormViewModel.cs

using BrahmsLab.Core.Interfaces;
using BrahmsLab.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class SpectralScanFormViewModel : ObservableObject
{
    private readonly ISpectralScanRepository _spectralScanRepository;

    // --- Bindable Properties for the Form ---

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveScanCommand))] // This tells the command to re-evaluate when this property changes
    public partial string? ExsicataIdentifier { get; set; }

    [ObservableProperty]
    public partial string? SpeciesName { get; set; }

    // Constructor: We ask for the repository service, and DI provides it.
    public SpectralScanFormViewModel(ISpectralScanRepository spectralScanRepository)
    {
        _spectralScanRepository = spectralScanRepository;
    }

    // --- Commands ---

    [RelayCommand(CanExecute = nameof(CanSaveScan))]
    private async Task SaveScanAsync()
    {
        // 1. Create a new domain model from the ViewModel's properties
        var newScan = new SpectralScan
        {
            ExsicataIdentifier = this.ExsicataIdentifier,
            SpeciesName = this.SpeciesName,
            SyncStatus = SyncStatus.New,
            Version = 1,
            UpdatedAtUtc = DateTime.UtcNow
        };

        // 2. Use the repository to save the data to the local database
        await _spectralScanRepository.AddAsync(newScan);

        // 3. (TODO for Module 2.3) Send a message to the rest of the app
        // WeakReferenceMessenger.Default.Send(new SpectralScanSavedMessage(newScan));

        // 4. Clear the form for the next entry
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