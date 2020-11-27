using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncoresoft.BindingExam.DataContext.Data
{
    internal class DataGenerator
    {
        internal static List<ControlTypeModel> GetControlList()
        {
            List<ControlTypeModel> controls = new List<ControlTypeModel>();
            controls.Add(new ControlTypeModel(typeof(Button)));
            controls.Add(new ControlTypeModel(typeof(TextBox)));
            controls.Add(new ControlTypeModel(typeof(TextBlock)));
            controls.Add(new ControlTypeModel(typeof(ComboBox)));
            controls.Add(new ControlTypeModel(typeof(CheckBox)));
            controls.Add(new ControlTypeModel(typeof(ListBox)));
            controls.Add(new ControlTypeModel(typeof(UserControl)));
            controls.Add(new ControlTypeModel(typeof(Window)));
            controls.Add(new ControlTypeModel(typeof(Border)));
            controls.Add(new ControlTypeModel(typeof(StackPanel)));
            controls.Add(new ControlTypeModel(typeof(DockPanel)));
            controls.Add(new ControlTypeModel(typeof(Viewbox)));
            controls.Add(new ControlTypeModel(typeof(Grid)));
            controls.Add(new ControlTypeModel(typeof(Canvas)));

            return controls;
        }
    }
}
