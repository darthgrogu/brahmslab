using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView.Painting;
using BrahmsLab.Core.Messages;
using BrahmsLab.Core.Models;
using Newtonsoft.Json;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class SpectralGraphViewModel : ObservableObject, IRecipient<SpectralScanSavedMessage>
{
    // Esta é a coleção de séries que nosso gráfico no XAML irá observar.
    // Qualquer alteração aqui (adicionar/remover uma série) será refletida no gráfico.
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

    // --- E AQUI TAMBÉM ---
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
    }

    public void Receive(SpectralScanSavedMessage message)
    {
        var newScan = message.Value;
        PlotScan(newScan);
    }

    private void PlotScan(SpectralScan scan)
    {
        // Limpa quaisquer séries anteriores
        Series.Clear();

        if (string.IsNullOrEmpty(scan.SpectrumJsonData)) return;

        // Deserializa os dados mockados
        var dataPoints = JsonConvert.DeserializeObject<List<double[]>>(scan.SpectrumJsonData);

        if (dataPoints != null)
        {
            // Adiciona uma nova série de linha à nossa coleção.
            // O gráfico na UI atualizará automaticamente.
            Series.Add(new LineSeries<double[]>
            {
                Values = dataPoints,
                Mapping = (dataPoint, index) => new(dataPoint[0], dataPoint[1]), // Mapeia o array [x, y] para um ponto no gráfico
                Fill = null,
                GeometrySize = 0,
                LineSmoothness = 0.5,
                Stroke = new SolidColorPaint(SKColors.CornflowerBlue) { StrokeThickness = 2 },
                Name = scan.ExsicataIdentifier
            });
        }
    }
}