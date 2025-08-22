using BrahmsLab.Core.Interfaces;
using BrahmsLab.Core.Messages;
using BrahmsLab.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class ScanHistoryViewModel : ObservableObject, IRecipient<SpectralScanSavedMessage>
{
    private readonly ISpectralScanRepository _scanRepository;

    // This is the collection that our DataGrid will bind to.
    // ObservableCollection automatically notifies the UI when items are added or removed.
    public ObservableCollection<SpectralScan> Scans { get; } = new();
    public ObservableCollection<object> SelectedScans { get; } = new();

    public ScanHistoryViewModel(ISpectralScanRepository scanRepository)
    {
        _scanRepository = scanRepository;
        WeakReferenceMessenger.Default.Register<SpectralScanSavedMessage>(this);

        SelectedScans.CollectionChanged += (s, e) => OnSelectionChanged();
    }

    // A method to load the data from the database.
    // We will call this when the component is loaded.
    public async Task LoadScansAsync()
    {
        Scans.Clear();
        var scansFromDb = await _scanRepository.GetAllAsync();
        foreach (var scan in scansFromDb)
        {
            Scans.Add(scan);
        }
    }

    public void Receive(SpectralScanSavedMessage message)
    {
        // Adiciona o novo scan à nossa coleção. A UI atualizará sozinha!
        Scans.Add(message.Value);
    }

    private void OnSelectionChanged()
    {
        // Convertemos a lista de 'object' para uma lista de 'SpectralScan'
        var selected = SelectedScans.OfType<SpectralScan>().ToList();

        // Enviamos a mensagem com a lista atual de scans selecionados.
        WeakReferenceMessenger.Default.Send(new SpectralScanSelectionChangedMessage(selected));
    }
}