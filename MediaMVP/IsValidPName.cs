using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace MediaMVP
{
    class IsValidPName : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            String name = value[0] as String;
            Grid grid = value[1] as Grid;
            if (grid == null) return false;
            MediaLoader media = grid.DataContext as MediaLoader;
            return !String.IsNullOrEmpty(name)&&!media.Sources.ContainsKey(name);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
