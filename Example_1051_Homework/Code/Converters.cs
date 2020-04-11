using Example_1051_Homework_10;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace DreamConvertions
{
    public class ConvertIsChecketToHidden : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool passedValue = (bool?)value == true;
            //return passedValue == true ? "True" : "False";
            string Inverse = parameter.ToString();
            if (Inverse.Equals("True"))
                return passedValue == true ? "Hidden" : "Visible";
            else
                return passedValue == true ? "Visible" : "Hidden";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        #endregion
    }

    public class ConverSelectedItemsToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? "False" : "True";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }


    public class UtcToLocalDateTimeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is DateTime)
                    return ((DateTime)value).ToLocalTime();
                else
                    return DateTime.Parse(value?.ToString()).ToLocalTime();
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        
    }


    public class TgMessageDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate
            SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is tgMessage)
            {
                tgMessage message = item as tgMessage;

                if (message.MessageType == tgMessageType.foto)
                    return
                        element.FindResource("PhotoMessageTemplate") as DataTemplate;
                else
                    return
                        element.FindResource("TextMessageTemplate") as DataTemplate;
            }

            return null;
        }
    }
}