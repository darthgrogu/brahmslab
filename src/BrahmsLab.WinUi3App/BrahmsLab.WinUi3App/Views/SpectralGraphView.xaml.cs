using Microsoft.UI.Xaml.Controls;
using BrahmsLab.WinUi3App.ViewModels;

namespace BrahmsLab.WinUi3App.Views
{
    public sealed partial class SpectralGraphView : UserControl
    {
        public SpectralGraphViewModel ViewModel => (SpectralGraphViewModel)this.DataContext;

        public SpectralGraphView()
        {
            InitializeComponent();
        }
    }
}
