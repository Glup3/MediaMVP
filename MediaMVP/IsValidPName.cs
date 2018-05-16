using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MediaMVP
{
    class IsValidPName : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            String name = value[0] as String;
            FrameworkElement element = value[1] as FrameworkElement;
            String param = null;
            if (value.Length>2)
            param = value[2] as string;
            if (element == null) return false;
            MediaLoader media = element.DataContext as MediaLoader;
            if(param==null)return !String.IsNullOrWhiteSpace(name)&&!media.Sources.ContainsKey(name.TrimEnd().TrimStart());
            else return !String.IsNullOrWhiteSpace(name) &&(name.TrimEnd().TrimStart().Equals(param) ||!media.Sources.ContainsKey(name.TrimEnd().TrimStart()));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
