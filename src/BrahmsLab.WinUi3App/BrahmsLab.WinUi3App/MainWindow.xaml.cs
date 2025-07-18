using BrahmsLab.WinUi3App.ViewModels; // <-- Garanta que este using est� aqui
using Microsoft.UI.Xaml;

namespace BrahmsLab.WinUi3App;

public sealed partial class MainWindow : Window
{
    public MainWindowViewModel ViewModel { get; }

    // O ViewModel n�o � mais criado aqui com "new".
    // Ele � FORNECIDO pelo container de DI quando a janela � criada.
    public MainWindow(MainWindowViewModel viewModel)
    {
        this.InitializeComponent();
        ViewModel = viewModel;
        this.Title = "BRAHMS Lab";
    }
}