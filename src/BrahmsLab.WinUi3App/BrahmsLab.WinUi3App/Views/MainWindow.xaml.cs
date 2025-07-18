using BrahmsLab.WinUi3App.ViewModels; // <-- Garanta que este using est� aqui
using WinUIEx;

namespace BrahmsLab.WinUi3App;

public sealed partial class MainWindow : WinUIEx.WindowEx
{
    public MainWindowViewModel ViewModel { get; }

    // O ViewModel n�o � mais criado aqui com "new".
    // Ele � FORNECIDO pelo container de DI quando a janela � criada.
    public MainWindow(MainWindowViewModel viewModel)
    {
        this.InitializeComponent();
        ViewModel = viewModel;
    }
}