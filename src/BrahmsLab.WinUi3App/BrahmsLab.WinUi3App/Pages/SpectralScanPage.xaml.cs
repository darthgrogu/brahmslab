using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace BrahmsLab.WinUi3App.Pages;

public sealed partial class SpectralScanPage : Page
{
    public SpectralScanPageViewModel ViewModel { get; }

    public SpectralScanPage()
    {
        InitializeComponent();
        ViewModel = App.Services.GetRequiredService<SpectralScanPageViewModel>();
    }
}
