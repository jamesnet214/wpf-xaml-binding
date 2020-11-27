using NcoreExam.DataContextTest.Extensions;
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

namespace NcoreExam.DataContextTest.Converters
{
    class TreeViewItemLeftMarginConverter : IValueConverter
    {
        public int Length { get; set; }
        public int Minus { get; set; } = 0;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var level = 0;
            if (value is DependencyObject)
            {
                var parent = VisualTreeHelper.GetParent(value as DependencyObject);
                while (!(parent is TreeView) && (parent != null))
                {
                    if (parent is TreeViewItem)
                        level++;
                    parent = VisualTreeHelper.GetParent(parent);
                }
            }
            var left = (level - Minus) * Length;
            return new Thickness(left, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}