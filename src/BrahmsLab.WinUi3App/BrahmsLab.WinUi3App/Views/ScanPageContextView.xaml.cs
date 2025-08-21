using BrahmsLab.WinUi3App.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace BrahmsLab.WinUi3App.Views;

public sealed partial class ScanPageContextView : UserControl
{
    // Este UserControl n�o precisa de um ViewModel por enquanto,
    // pois os seus filhos (`SessionWidgetView` e `DeviceWidgetView`)
    // j� obt�m os seus pr�prios ViewModels a partir do DI Container.
    public ScanPageContextView()
    {
        this.InitializeComponent();
    }
}