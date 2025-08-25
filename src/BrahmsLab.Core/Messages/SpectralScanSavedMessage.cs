using BrahmsLab.Core.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BrahmsLab.Core.Messages;

// A mensagem herda de ValueChangedMessage<T> e simplesmente carrega o objeto salvo.
public class SpectralScanSavedMessage : ValueChangedMessage<LocalSpectralReading>
{
    public SpectralScanSavedMessage(LocalSpectralReading value) : base(value) { }
}