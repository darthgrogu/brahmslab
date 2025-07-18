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

        // A navegação inicial agora é mais robusta
        NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems.OfType<NavigationViewItem>().First();
        ContentFrame.Navigate(typeof(Pages.SpectralScanPage), null, new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
    }

    private void NavigationViewControl_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        // Esta lógica é adaptada diretamente do exemplo
        if (args.IsSettingsInvoked == true)
        {
            // TODO: Navigate to settings page
        }
        else if (args.InvokedItemContainer?.Tag is string pageTag)
        {
            // Usamos a correção de tipo anulável que descobrimos
            Type? newPage = Type.GetType(pageTag);
            if (newPage != null && ContentFrame.CurrentSourcePageType != newPage)
            {
                ContentFrame.Navigate(newPage, null, args.RecommendedNavigationTransitionInfo);
            }
        }
    }

    private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
    {
        // A lógica de sincronização da UI após a navegação
        NavigationViewControl.IsBackEnabled = ContentFrame.CanGoBack;

        if (ContentFrame.SourcePageType == typeof(Pages.SettingsPage)) // Exemplo para o futuro
        {
            NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;
        }
        else if (ContentFrame.SourcePageType != null)
        {
            // Encontra o item de menu cujo Tag corresponde à página atual e o seleciona
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