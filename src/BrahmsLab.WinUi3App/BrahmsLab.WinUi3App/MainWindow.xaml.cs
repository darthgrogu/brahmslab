using BrahmsLab.WinUi3App.ViewModels; // <-- Garanta que este using está aqui
using Microsoft.UI.Xaml;

namespace BrahmsLab.WinUi3App;

public sealed partial class MainWindow : Window
{
    public MainWindowViewModel ViewModel { get; }

    // O ViewModel não é mais criado aqui com "new".
    // Ele é FORNECIDO pelo container de DI quando a janela é criada.
    public MainWindow(MainWindowViewModel viewModel)
    {
        this.InitializeComponent();
        ViewModel = viewModel;
        this.Title = "BRAHMS Lab";
    }
}