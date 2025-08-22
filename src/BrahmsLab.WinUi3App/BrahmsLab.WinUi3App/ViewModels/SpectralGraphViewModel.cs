using BrahmsLab.Core.Messages;
using BrahmsLab.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Newtonsoft.Json;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class SpectralGraphViewModel : ObservableObject,
    IRecipient<SpectralScanSavedMessage>,
    IRecipient<SpectralScanSelectionChangedMessage>
{
    private readonly List<SKColor> _colorPalette = new()
    {
        SKColors.CornflowerBlue,
        SKColors.IndianRed,
        SKColors.MediumSeaGreen,
        SKColors.Orange,
        SKColors.MediumPurple,
        SKColors.Gold,
        SKColors.Cyan
    };

    // 2. Um índice para acompanhar qual cor usar.
    private int _currentColorIndex = 0;

    public ObservableCollection<ISeries> Series { get; set; }

    public ICartesianAxis[] XAxes { get; set; } =
     {
        new Axis
        {
            Name = "Wavelength (nm)",
            NamePaint = new SolidColorPaint(SKColors.White), // Melhora a visibilidade em tema escuro
            LabelsPaint = new SolidColorPaint(SKColors.White),
            SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 0.5f }
        }
    };

    public ICartesianAxis[] YAxes { get; set; } =
    {
        new Axis
        {
            Name = "Reflectance",
            NamePaint = new SolidColorPaint(SKColors.White),
            LabelsPaint = new SolidColorPaint(SKColors.White),
            SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 0.5f }
        }
    };

    public SpectralGraphViewModel()
    {
        Series = new ObservableCollection<ISeries>();
        WeakReferenceMessenger.Default.Register<SpectralScanSavedMessage>(this);
        WeakReferenceMessenger.Default.Register<SpectralScanSelectionChangedMessage>(this);
    }

    public void Receive(SpectralScanSavedMessage message)
    {
        PlotScans(new List<SpectralScan> { message.Value });
    }

    // ESTE É O NOVO MÉTODO que será chamado quando a seleção na lista mudar
    public void Receive(SpectralScanSelectionChangedMessage message)
    {
        // A mensagem carrega a lista de scans selecionados
        var selectedScans = message.Value;
        PlotScans(selectedScans);
    }

    // Refatoramos a lógica de plotagem para um método reutilizável
    private void PlotScans(List<SpectralScan> scansToPlot)
    {
        Series.Clear();
        _currentColorIndex = 0; // Reseta o índice de cor toda vez que a plotagem é refeita

        if (scansToPlot == null || !scansToPlot.Any())
        {
            return;
        }

        foreach (var scan in scansToPlot)
        {
            if (string.IsNullOrEmpty(scan.SpectrumJsonData)) continue;

            var dataPoints = JsonConvert.DeserializeObject<List<double[]>>(scan.SpectrumJsonData);
            if (dataPoints != null)
            {
                // 3. Pega a próxima cor da paleta
                var currentColor = GetNextColor();

                Series.Add(new LineSeries<double[]>
                {
                    Values = dataPoints,
                    Mapping = (dataPoint, index) => new(dataPoint[0], dataPoint[1]),
                    Fill = null,
                    GeometrySize = 0,
                    LineSmoothness = 0.5,
                    // Usa a cor selecionada para a linha
                    Stroke = new SolidColorPaint(currentColor) { StrokeThickness = 2 },
                    Name = "scan.ExsicataIdentifier"
                });
            }
        }
    }

    // 4. Método helper para pegar a próxima cor de forma rotativa
    private SKColor GetNextColor()
    {
        var color = _colorPalette[_currentColorIndex];
        _currentColorIndex++;
        if (_currentColorIndex >= _colorPalette.Count)
        {
            _currentColorIndex = 0;
        }
        return color;
    }
}