using BrahmsLab.WinUi3App.ViewModels;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BrahmsLab.WinUi3App
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }

        public MainWindow()
        {
            this.InitializeComponent();

            // Instantiate the ViewModel.
            ViewModel = new MainViewModel();

            // We can set the initial window title from here.
            this.Title = "BRAHMS Lab";
        }
    }
}
