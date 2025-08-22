// File: src/4_Shells/BrahmsLab.WinUi3App/ViewModels/SelectableFlagViewModel.cs
using Brahms.Api.Contracts.Enums.iHerbSpec;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class SelectableFlagViewModel : ObservableObject
{
    // A flag Enum que este ViewModel representa (ex: MeasurementFlags.HerbivoryPresent)
    public MeasurementFlags Flag { get; }

    // O texto a ser exibido no botão (ex: "HerbivoryPresent")
    public string DisplayName { get; }

    [ObservableProperty]
    public partial bool IsSelected { get; set;}

    public SelectableFlagViewModel(MeasurementFlags flag)
    {
        Flag = flag;
        DisplayName = flag.ToString();
    }
}