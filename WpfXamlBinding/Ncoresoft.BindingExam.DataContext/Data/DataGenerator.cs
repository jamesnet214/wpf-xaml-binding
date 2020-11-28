using Ncoresoft.BindingExam.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ncoresoft.BindingExam.DataContext.Data
{
    internal class DataGenerator
    {
        internal static List<WPFControlModel> GetControlList()
        {
            List<WPFControlModel> controls = new List<WPFControlModel>();
            controls.Add(new WPFControlModel(typeof(Button)));
            controls.Add(new WPFControlModel(typeof(TextBox)));
            controls.Add(new WPFControlModel(typeof(TextBlock)));
            controls.Add(new WPFControlModel(typeof(ComboBox)));
            controls.Add(new WPFControlModel(typeof(CheckBox)));
            controls.Add(new WPFControlModel(typeof(ListBox)));
            controls.Add(new WPFControlModel(typeof(UserControl)));
            controls.Add(new WPFControlModel(typeof(Window)));
            controls.Add(new WPFControlModel(typeof(Border)));
            controls.Add(new WPFControlModel(typeof(StackPanel)));
            controls.Add(new WPFControlModel(typeof(DockPanel)));
            controls.Add(new WPFControlModel(typeof(Viewbox)));
            controls.Add(new WPFControlModel(typeof(Grid)));
            controls.Add(new WPFControlModel(typeof(Canvas)));

            return controls;
        }
    }
}
