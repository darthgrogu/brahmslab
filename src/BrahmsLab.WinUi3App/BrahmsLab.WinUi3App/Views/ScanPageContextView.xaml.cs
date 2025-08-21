using BrahmsLab.WinUi3App.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace BrahmsLab.WinUi3App.Views;

public sealed partial class ScanPageContextView : UserControl
{
    // Este UserControl não precisa de um ViewModel por enquanto,
    // pois os seus filhos (`SessionWidgetView` e `DeviceWidgetView`)
    // já obtêm os seus próprios ViewModels a partir do DI Container.
    public ScanPageContextView()
    {
        this.InitializeComponent();
    }
}