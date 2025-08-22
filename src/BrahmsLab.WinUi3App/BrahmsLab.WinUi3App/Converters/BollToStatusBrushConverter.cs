// File: src/4_Shells/BrahmsLab.WinUi3App/Converters/BooleanToStatusBrushConverter.cs
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;

namespace BrahmsLab.WinUi3App.Converters;

public class BooleanToStatusBrushConverter : IValueConverter
{
    // Este método é chamado quando os dados vão do ViewModel para a View.
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        // Verificamos se o valor de entrada é um booleano e se é 'true'.
        if (value is bool isConnected && isConnected)
        {
            // Se estiver conectado, retorna um pincel verde.
            return new SolidColorBrush(Colors.Green);
        }
        else
        {
            // Caso contrário, retorna um pincel cinzento.
            return new SolidColorBrush(Colors.Gray);
        }
    }

    // Este método não é necessário para o nosso caso, pois o binding é OneWay.
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}