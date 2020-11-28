using NcoreExam.Common;
using Ncoresoft.BindingExam.DataContext.Data;
using Ncoresoft.BindingExam.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncoresoft.BindingExam.DataContext.ViewModels
{
    public class ControlViewModel : ObservableObject
    {
        private List<WPFControlModel> _controlSource;
        public List<WPFControlModel> ControlSource
        {
            get { return _controlSource; }
            set { _controlSource = value; OnPropertyChanged(); }
        }

        private WPFControlModel _currentControl;
        public WPFControlModel CurrentControl
        {
            get { return _currentControl; }
            set { _currentControl = value; OnPropertyChanged(); }
        }

        public ControlViewModel()
        {
            ControlSource = DataGenerator.GetControlList();
        }
    }
}
