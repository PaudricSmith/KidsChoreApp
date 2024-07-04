using System.Globalization;


namespace KidsChoreApp.Converters
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                Console.WriteLine("CurrencyConverter: Value is null");
                return string.Empty;
            }
            if (parameter == null)
            {
                Console.WriteLine("CurrencyConverter: Parameter is null");
                return value;
            }
            if (value is decimal money && parameter is string currency)
            {
                Console.WriteLine($"CurrencyConverter: Converting {money} to {currency}");
                var cultureInfo = new CultureInfo(culture.Name)
                {
                    NumberFormat = { CurrencySymbol = GetCurrencySymbol(currency) }
                };
                return string.Format(cultureInfo, "{0:C}", money);
            }
            Console.WriteLine($"CurrencyConverter: Invalid value or parameter. Value: {value}, Parameter: {parameter}");
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetCurrencySymbol(string currency)
        {
            return currency switch
            {
                "USD" => "$",
                "EUR" => "€",
                "GBP" => "£",
                _ => "$"
            };
        }
    }
}


//using System;
//using System.Globalization;
//using Microsoft.Maui.Controls;

//namespace KidsChoreApp.Converters
//{
//    public class CurrencyConverter : IMultiValueConverter
//    {
//        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
//        {
//            if (values[0] is decimal money && values[1] is string currency)
//            {
//                Console.WriteLine($"CurrencyMultiConverter: Converting {money} to {currency}");
//                var cultureInfo = new CultureInfo(culture.Name)
//                {
//                    NumberFormat = { CurrencySymbol = GetCurrencySymbol(currency) }
//                };
//                return string.Format(cultureInfo, "{0:C}", money);
//            }
//            Console.WriteLine($"CurrencyMultiConverter: Invalid value or parameter. Money: {values[0]}, Currency: {values[1]}");
//            return values[0];
//        }

//        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }

//        private string GetCurrencySymbol(string currency)
//        {
//            return currency switch
//            {
//                "USD" => "$",
//                "EUR" => "€",
//                "GBP" => "£",
//                _ => "$"
//            };
//        }
//    }
//}
