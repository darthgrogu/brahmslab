using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public SpectralScanFormViewModel SpectralScanForm { get; }

    [ObservableProperty]
    public partial string? StatusText { get; set; }

    public MainWindowViewModel(SpectralScanFormViewModel spectralScanFormViewModel)
    {
        SpectralScanForm = spectralScanFormViewModel;

        StatusText = "System Ready. Conventions Applied!";
    }

    [RelayCommand]
    private void UpdateStatus()
    {
        // E aqui também usamos a propriedade pública para alterar o valor.
        StatusText = $"Command executed at {DateTime.Now:HH:mm:ss}";
    }
}