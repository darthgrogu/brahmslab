// ViewModels/MainViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    // A anotação na propriedade pública, parcial e com PascalCase.
    // Esta é a forma correta e recomendada para WinUI 3.
    [ObservableProperty]
    public partial string? StatusText { get; set; }

    public MainWindowViewModel()
    {
        // Acessamos a propriedade pública para definir o valor inicial.
        StatusText = "System Ready. Conventions Applied!";
    }

    [RelayCommand]
    private void UpdateStatus()
    {
        // E aqui também usamos a propriedade pública para alterar o valor.
        StatusText = $"Command executed at {DateTime.Now:HH:mm:ss}";
    }
}