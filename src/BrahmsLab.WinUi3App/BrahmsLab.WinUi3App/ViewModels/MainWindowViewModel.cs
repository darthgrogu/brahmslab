using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public SpectralScanFormViewModel SpectralScanForm { get; }

    public MainWindowViewModel(SpectralScanFormViewModel spectralScanFormViewModel)
    {
        SpectralScanForm = spectralScanFormViewModel;

    }
}