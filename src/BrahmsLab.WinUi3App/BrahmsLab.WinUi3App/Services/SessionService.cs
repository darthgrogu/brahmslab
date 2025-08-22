using BrahmsLab.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace BrahmsLab.WinUi3App.Services;

public partial class SessionService : ObservableObject, ISessionService
{
    [ObservableProperty]
    public partial bool IsDeviceConnected { get; set; }
    
    public async Task ConnectDeviceAsync()
    {
        // Simula uma operação de rede/hardware que demora 1.5 segundos
        await Task.Delay(1500);

        IsDeviceConnected = true;

        // TODO: Acionar o diálogo de "Nova Sessão" aqui.
    }

    public void DisconnectDevice()
    {
        IsDeviceConnected = false;
    }
}