using BrahmsLab.WinUi3App.ViewModels; // <-- Garanta que este using está aqui
using WinUIEx;

namespace BrahmsLab.WinUi3App;

public sealed partial class MainWindow : WinUIEx.WindowEx
{
    public MainWindowViewModel ViewModel { get; }

    // O ViewModel não é mais criado aqui com "new".
    // Ele é FORNECIDO pelo container de DI quando a janela é criada.
    public MainWindow(MainWindowViewModel viewModel)
    {
        this.InitializeComponent();
        ViewModel = viewModel;
    }
}