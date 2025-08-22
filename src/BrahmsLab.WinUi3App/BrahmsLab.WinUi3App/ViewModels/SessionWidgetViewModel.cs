// File: src/4_Shells/BrahmsLab.WinUi3App/ViewModels/SessionWidgetViewModel.cs

using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BrahmsLab.WinUi3App.ViewModels;

public partial class SessionWidgetViewModel : ObservableObject
{
    // --- Propriedades de Dados da Sessão ---
    [ObservableProperty] public partial string ProjectId { get; set; }
    [ObservableProperty] public partial string Operator { get; set; }
    [ObservableProperty] public partial string InstrumentModel { get; set; }

    // --- Propriedades para os Mini-Gráficos ---
    public ObservableCollection<ISeries> WhiteReferenceSeries { get; set; }
    public ObservableCollection<ISeries> BlackBackgroundSeries { get; set; }

    public ICartesianAxis[] WhiteReferenceYAxes { get; } =
    {
        new Axis { IsVisible = false, MinLimit = 90, MaxLimit = 110 }
    };

    // Eixos para o gráfico de fundo preto (esperamos valores entre 0-10)
    public ICartesianAxis[] BlackBackgroundYAxes { get; } =
    {
        new Axis { IsVisible = false, MinLimit = 0, MaxLimit = 10 }
    };

    // Eixo X invisível, comum a ambos os gráficos
    public ICartesianAxis[] MiniChartXAxes { get; } = { new Axis { IsVisible = false } };


    public SessionWidgetViewModel()
    {
        ProjectId = "Spectral INPA";
        Operator = "DarthGrogu";
        InstrumentModel = "MICRONIR-SCR3";

        WhiteReferenceSeries = new ObservableCollection<ISeries>();
        BlackBackgroundSeries = new ObservableCollection<ISeries>();

        LoadMockReferenceScans();
    }

    private void LoadMockReferenceScans()
    {
        var rand = new Random();

        // Simula uma Referência Branca com uma leve curvatura descendente
        var whiteRefData = new List<double[]>();
        for (int i = 0; i < 20; i++)
        {
            // Uma curva suave com um pouco de ruído
            double y = 100.5 - (i * 0.02) + (rand.NextDouble() - 1.5) * 2.5;
            whiteRefData.Add(new[] { (double)i, y });
        }

        WhiteReferenceSeries.Clear(); // Limpa dados antigos
        WhiteReferenceSeries.Add(new LineSeries<double[]>
        {
            Values = whiteRefData,
            Mapping = (point, index) => new(point[0], point[1]),
            Fill = null,
            GeometrySize = 0,
            Stroke = new SolidColorPaint(SKColors.LightGray) { StrokeThickness = 2 }
        });

        // Simula um Fundo Preto com um pequeno pico (artefato comum)
        var blackBgData = new List<double[]>();
        for (int i = 0; i < 20; i++)
        {
            // Uma linha base com um pico no meio
            double peak = 2 * Math.Exp(-Math.Pow(i - 50, 2) / (2 * Math.Pow(10, 2))); // Fórmula de um pico Gaussiano
            double y = 2.5 + peak + (rand.NextDouble() - 0.5) * 0.4;
            blackBgData.Add(new[] { (double)i, y });
        }

        BlackBackgroundSeries.Clear(); // Limpa dados antigos
        BlackBackgroundSeries.Add(new LineSeries<double[]>
        {
            Values = blackBgData,
            Mapping = (point, index) => new(point[0], point[1]),
            Fill = null,
            GeometrySize = 0,
            Stroke = new SolidColorPaint(SKColors.DimGray) { StrokeThickness = 2 }
        });
    }
}