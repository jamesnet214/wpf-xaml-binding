using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Ncoresoft.BindingExam.DataContext.Converters
{
    class LeftMarginCalculatingConverter : IValueConverter
    {
        public int Length { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int level = 0;
            if (value is DependencyObject d)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(d);
                
                while (!(parent is TreeView))
                {
                    if (parent is TreeViewItem)
                    {
                        level++;
                    }
                    parent = VisualTreeHelper.GetParent(parent);
                }
            }
            return new Thickness(level * Length, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}