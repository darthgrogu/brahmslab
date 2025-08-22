using BrahmsLab.WinUi3App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace BrahmsLab.WinUi3App.Views;

public sealed partial class SessionWidgetView : UserControl
{
    public SessionWidgetViewModel ViewModel { get; }

    public SessionWidgetView()
    {
        this.InitializeComponent();
        ViewModel = App.Services.GetRequiredService<SessionWidgetViewModel>();
    }
}