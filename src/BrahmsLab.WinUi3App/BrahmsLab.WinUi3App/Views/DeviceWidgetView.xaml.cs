using BrahmsLab.WinUi3App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace BrahmsLab.WinUi3App.Views;

public sealed partial class DeviceWidgetView : UserControl
{
    // A propriedade ViewModel para o {x:Bind} funcionar.
    // Usamos GetRequiredService aqui para obter uma instância.
    public DeviceWidgetViewModel ViewModel { get; }

    public DeviceWidgetView()
    {
        this.InitializeComponent();
        ViewModel = App.Services.GetRequiredService<DeviceWidgetViewModel>();
    }
}