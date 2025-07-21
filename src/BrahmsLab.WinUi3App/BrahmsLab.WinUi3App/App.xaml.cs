// File: src/4_Shells/BrahmsLab.WinUi3App/App.xaml.cs

using BrahmsLab.Core.Interfaces;
using BrahmsLab.DataAccess.Data;
using BrahmsLab.DataAccess.Repositories;
using BrahmsLab.WinUi3App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System;

namespace BrahmsLab.WinUi3App;

public partial class App : Application
{
    private Window? _window;

    public static new App Current { get; private set; } = default!;

    public IHost Host { get; }

    // Propriedade estática para fácil acesso aos serviços em toda a aplicação
    public static IServiceProvider Services => Current.Host.Services;

    public App()
    {
        InitializeComponent();

        Current = this;

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Register ViewModels
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<SpectralScanFormViewModel>();
            services.AddTransient<SpectralScanPageViewModel>();
            services.AddSingleton<ScanHistoryViewModel>();
            services.AddSingleton<SpectralGraphViewModel>();

            // Register Data Services
            services.AddDbContext<BrahmsLabDbContext>();
            services.AddScoped<ISpectralScanRepository, SpectralScanRepository>();

            // Register Views (our Windows)
            services.AddTransient<MainWindow>();
        }).
        Build();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _window = Services.GetService<MainWindow>();

        if (_window == null)
        {
            throw new InvalidOperationException("Main window could not be resolved from the service provider.");
        }

        _window.Activate();
    }
}