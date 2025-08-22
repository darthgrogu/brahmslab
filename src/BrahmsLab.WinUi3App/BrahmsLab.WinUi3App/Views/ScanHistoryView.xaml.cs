using BrahmsLab.WinUi3App.ViewModels;
using CommunityToolkit.WinUI.UI.Controls;
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificamos se o ViewModel j� existe para evitar erros durante a inicializa��o.
            if (ViewModel != null)
            {
                // Primeiro, limpamos a cole��o de sele��o antiga no ViewModel.
                ViewModel.SelectedScans.Clear();

                // 'sender' � o pr�prio DataGrid que disparou o evento.
                var grid = (DataGrid)sender;

                // Percorremos os itens selecionados no DataGrid e os adicionamos
                // um por um � nossa cole��o observ�vel no ViewModel.
                foreach (var item in grid.SelectedItems)
                {
                    ViewModel.SelectedScans.Add(item);
                }
            }
        }
    }
}
