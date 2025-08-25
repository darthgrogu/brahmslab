using BrahmsLab.Core.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.Generic;

namespace BrahmsLab.Core.Messages;

public class SpectralScanSelectionChangedMessage : ValueChangedMessage<List<LocalSpectralReading>>
{
    public SpectralScanSelectionChangedMessage(List<LocalSpectralReading> value) : base(value) { }
}