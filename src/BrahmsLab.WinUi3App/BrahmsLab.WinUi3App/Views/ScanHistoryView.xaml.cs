using BrahmsLab.WinUi3App.ViewModels;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BrahmsLab.WinUi3App.Views
{
    public sealed partial class ScanHistoryView : UserControl
    {
        public ScanHistoryViewModel ViewModel => (ScanHistoryViewModel)this.DataContext;

        public ScanHistoryView()
        {
            InitializeComponent();
            this.Loaded += ScanHistoryView_LoadedAsync;
        }

        private async void ScanHistoryView_LoadedAsync(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadScansAsync();
        }
    }
}
