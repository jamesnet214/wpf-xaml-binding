using NcoreExam.Common;
using NcoreExam.DataContextTest.Data;
using NcoreExam.DataContextTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcoreExam.DataContextTest.ViewModels
{
    public class ControlExplorerViewModel : ObservableObject
    {
        private ControlTypeModel _currentType;
        public ControlTypeModel CurrentType
        {
            get { return _currentType; }
            set { _currentType = value; OnPropertyChanged(); TypeChanged(value); }
        }

        private List<ControlTypeModel> _controlType;
        public List<ControlTypeModel> ControlType
        {
            get { return _controlType; }
            set { _controlType = value; OnPropertyChanged(); }
        }

        private List<BaseClassModel> _baseTypes;
        public List<BaseClassModel> BaseTypes
        {
            get { return _baseTypes; }
            set { _baseTypes = value; OnPropertyChanged(); }
        }

        public ControlExplorerViewModel()
        {
            ControlType = DataGenerator.GetControlList();
        }

        private BaseClassModel CreateClassModel(Type type)
        {
            BaseClassModel item = new BaseClassModel { Type = type, BaseTypes = new List<BaseClassModel>() };

            if (type.BaseType != null)
            {
                item.BaseTypes.Add(CreateClassModel(type.BaseType));
            }

            return item;
        }

        private void TypeChanged(ControlTypeModel value)
        {
            List<BaseClassModel> source = new List<BaseClassModel>();

            BaseClassModel selectedItem = CreateClassModel(value.ControlType);

            if (selectedItem != null)
            {
                source.Add(selectedItem);
            }

            BaseTypes = source;
        }
    }
}
