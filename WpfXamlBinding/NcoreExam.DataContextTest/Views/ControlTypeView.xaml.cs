using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using NcoreExam.DataContextTest.Data;
using NcoreExam.DataContextTest.Models;
using NcoreExam.DataContextTest.ViewModels;

namespace NcoreExam.DataContextTest.Views
{
    public partial class ControlTypeView : UserControl
    {
        public ControlTypeView()
        {
            InitializeComponent();
            DataContext = new ControlExplorerViewModel();
        }
    }
}
