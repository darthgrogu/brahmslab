// File: src/4_Shells/BrahmsLab.WinUi3App/MainWindow.xaml.cs
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;
using WinUIEx;

namespace BrahmsLab.WinUi3App;

public sealed partial class MainWindow : WindowEx
{
    public MainWindow()
    {
        this.InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);

        // A navega��o inicial agora � mais robusta
        NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems.OfType<NavigationViewItem>().First();
        ContentFrame.Navigate(typeof(Pages.SpectralScanPage), null, new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
    }

    private void NavigationViewControl_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        // Esta l�gica � adaptada diretamente do exemplo
        if (args.IsSettingsInvoked == true)
        {
            // TODO: Navigate to settings page
        }
        else if (args.InvokedItemContainer?.Tag is string pageTag)
        {
            // Usamos a corre��o de tipo anul�vel que descobrimos
            Type? newPage = Type.GetType(pageTag);
            if (newPage != null && ContentFrame.CurrentSourcePageType != newPage)
            {
                ContentFrame.Navigate(newPage, null, args.RecommendedNavigationTransitionInfo);
            }
        }
    }

    private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
    {
        // A l�gica de sincroniza��o da UI ap�s a navega��o
        NavigationViewControl.IsBackEnabled = ContentFrame.CanGoBack;

        if (ContentFrame.SourcePageType == typeof(Pages.SettingsPage)) // Exemplo para o futuro
        {
            NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;
        }
        else if (ContentFrame.SourcePageType != null)
        {
            // Encontra o item de menu cujo Tag corresponde � p�gina atual e o seleciona
            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault(n => n.Tag.ToString() == ContentFrame.SourcePageType.FullName);
        }
    }

    private void NavigationViewControl_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
    {
        if (ContentFrame.CanGoBack)
        {
            ContentFrame.GoBack();
        }
    }
}