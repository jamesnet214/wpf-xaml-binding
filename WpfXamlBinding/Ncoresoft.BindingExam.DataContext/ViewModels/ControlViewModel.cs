using NcoreExam.Common;
using Ncoresoft.BindingExam.DataContext.Data;
using Ncoresoft.BindingExam.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ncoresoft.BindingExam.DataContext.ViewModels
{
    public class ControlViewModel : ObservableObject
    {
        #region ControlSource

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
            set { _currentControl = value; OnPropertyChanged(); ControlChanged(value); }
        }
        #endregion

        #region BaseControls

        private List<WPFControlModel> _baseControls;
        public List<WPFControlModel> BaseControls
        {
            get { return _baseControls; }
            set { _baseControls = value; OnPropertyChanged(); }
        }
        #endregion

        #region Properties

        private List<PropertyInfo> _properties;
        public List<PropertyInfo> Properties
        {
            get { return _properties; }
            set { _properties = value; OnPropertyChanged(); }
        }
        #endregion

        public ICommand SelectedCommand { get; set; }

        #region Constructor

        public ControlViewModel()
        {
            SelectedCommand = new RelayCommand<WPFControlModel>(ExecSelected);

            ControlSource = DataGenerator.GetControlList();
            CurrentControl = ControlSource.First();
        }
        #endregion

        #region ControlChanged

        private WPFControlModel CreateClassModel(Type type)
        {
            WPFControlModel item = new WPFControlModel(type) { Controls = new List<WPFControlModel>() };

            if (type.BaseType != null)
            {
                item.Controls.Add(CreateClassModel(type.BaseType));
            }

            return item;
        }

        private void ControlChanged(WPFControlModel value)
        {
            List<WPFControlModel> source = new List<WPFControlModel>();

            WPFControlModel selectedItem = CreateClassModel(value.Control);

            if (selectedItem != null)
            {
                source.Add(selectedItem);
            }

            BaseControls = source;
        }
        #endregion

        #region ExecSelected

        private void ExecSelected(WPFControlModel item)
        {
            Properties = item.Control.GetProperties().Where(x => x.DeclaringType.FullName == item.Control.FullName).OrderBy(x => x.Name).ToList();
        }
        #endregion
    }
}
