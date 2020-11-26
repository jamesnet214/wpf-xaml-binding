using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using NcoreExam.DataContextTest.Data;
using NcoreExam.DataContextTest.Models;

namespace NcoreExam.DataContextTest.Views
{
    public partial class ControlTypeView : UserControl
    {
        public ControlTypeView()
        {
            InitializeComponent();

            lb.ItemsSource = DataGenerator.GetControlList();
            lb.SelectionChanged += Lb_SelectionChanged;
        }

        private void Lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lbx = sender as ListBox;
            ControlTypeModel item = lbx.SelectedItem as ControlTypeModel;

            var b = item.ControlType.BaseType;
        }
    }
}
