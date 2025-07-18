using BrahmsLab.WinUi3App.ViewModels; // Importante
using Microsoft.UI.Xaml.Controls;

namespace BrahmsLab.WinUi3App.Views;

public sealed partial class SpectralScanFormView : UserControl
{
    // Criamos uma propriedade ViewModel fortemente tipada que simplesmente
    // faz o "cast" do DataContext. Isso dá ao {x:Bind} a inteligência
    // de saber os tipos e propriedades em tempo de compilação.
    public SpectralScanFormViewModel ViewModel => (SpectralScanFormViewModel)this.DataContext;

    public SpectralScanFormView()
    {
        this.InitializeComponent();
    }
}