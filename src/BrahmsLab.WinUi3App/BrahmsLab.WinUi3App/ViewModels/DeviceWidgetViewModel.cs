// File: src/4_Shells/BrahmsLab.WinUi3App/ViewModels/DeviceWidgetViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class DeviceWidgetViewModel : ObservableObject
{
    public DeviceWidgetViewModel()
    {
        DeviceName = "MicroNIR OnSite-W";
        DeviceStatus = "Connected";
        BatteryLevel = 0.85;
    }

    public string DeviceName { get; set; }
    public string DeviceStatus { get; set; }
    public double BatteryLevel { get; set; }
}