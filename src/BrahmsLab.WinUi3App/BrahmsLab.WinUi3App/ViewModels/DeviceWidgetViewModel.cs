// File: src/4_Shells/BrahmsLab.WinUi3App/ViewModels/DeviceWidgetViewModel.cs

using BrahmsLab.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class DeviceWidgetViewModel : ObservableObject
{
    private readonly ISessionService _sessionService;

    public bool IsDeviceConnected => _sessionService.IsDeviceConnected;
    public string DeviceName => "MicroNIR OnSite-W (Simulated)";
    public string DeviceStatus => IsDeviceConnected ? "Connected" : "Disconnected";

    public string ConnectButtonText => IsDeviceConnected ? "Disconnect" : "Connect";

    //Controla se uma operação está em andamento (para desabilitar o botão).
    [ObservableProperty] public partial bool IsBusy { get; set;}

    public DeviceWidgetViewModel(ISessionService sessionService)
    {
        _sessionService = sessionService;

        _sessionService.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(ISessionService.IsDeviceConnected))
            {
                OnPropertyChanged(nameof(IsDeviceConnected));
                OnPropertyChanged(nameof(DeviceStatus));

                // Notifica a UI que o texto do botão também precisa de ser atualizado.
                OnPropertyChanged(nameof(ConnectButtonText));

                // Notifica os comandos para reavaliarem o seu estado.
                ToggleConnectionCommand.NotifyCanExecuteChanged();
            }
        };
    }

    [RelayCommand(CanExecute = nameof(CanToggleConnection))]
    private async Task ToggleConnectionAsync()
    {
        IsBusy = true; // Desabilita o botão
        try
        {
            if (IsDeviceConnected)
            {
                // Se já estiver conectado, desconecta.
                _sessionService.DisconnectDevice();
            }
            else
            {
                // Se não, conecta.
                await _sessionService.ConnectDeviceAsync();
            }
        }
        finally
        {
            IsBusy = false; // Reabilita o botão, quer a operação tenha sucesso ou falhe.
        }
    }

    // A lógica de validação agora verifica o estado "ocupado".
    private bool CanToggleConnection()
    {
        return !IsBusy;
    }
}