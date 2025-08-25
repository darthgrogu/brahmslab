/*using BrahmsLab.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class SessionViewModel : ObservableObject
{
    private readonly Session _session; // A entidade de dados real

    // Propriedades formatadas para exibição na UI
    public string DisplayId => $"Session: {_session.SessionId}";
    public string ProjectInfo => $"Project: {_session.ProjectId}";
    public string OperatorInfo => $"Operator: {_session.Operator}";
    public int ScanCount => _session.SpectralScans?.Count ?? 0;

    public SessionViewModel(Session session)
    {
        _session = session;
    }
}*/