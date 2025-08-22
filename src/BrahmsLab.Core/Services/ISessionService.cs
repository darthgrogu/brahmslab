using System.ComponentModel;

namespace BrahmsLab.Core.Services;

public interface ISessionService : INotifyPropertyChanged
{
    bool IsDeviceConnected { get; }
    // Futuramente, teremos: public Session ActiveSession { get; }

    Task ConnectDeviceAsync();
    void DisconnectDevice();
}