// Em SpectralScanPageViewModel.cs
using BrahmsLab.WinUi3App.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

public partial class SpectralScanPageViewModel : ObservableObject
{
    public SpectralScanFormViewModel SpectralScanForm { get; }
    public ScanHistoryViewModel ScanHistory { get; }
    public SpectralGraphViewModel Graph { get; }

    public SpectralScanPageViewModel(SpectralScanFormViewModel spectralScanForm, ScanHistoryViewModel scanHistory, SpectralGraphViewModel graph)
    {
        SpectralScanForm = spectralScanForm;
        ScanHistory = scanHistory;
        Graph = graph;
    }
}