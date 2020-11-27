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
        public List<BaseClassModel> _baseTypes;

        public List<BaseClassModel> BaseTypes
        {
            get { return _baseTypes; }
            set { _baseTypes = value; OnPropertyChanged(); }
        }


        public ControlExplorerViewModel()
        {
            ControlType = DataGenerator.GetControlList();
        }

        private void TypeChanged(ControlTypeModel value)
        {
            List<BaseClassModel> source = new List<BaseClassModel>();

            BaseClassModel createClassModel(Type type)
            {
                BaseClassModel item = new BaseClassModel { Type = type, BaseTypes = new List<BaseClassModel>() };                

                if (type.BaseType != null)
                {
                    item.BaseTypes.Add(createClassModel(type.BaseType));
                }

                return item;
            }

            var item1 = createClassModel(value.ControlType);

            if (item1 != null)
            {
                source.Add(item1);
            }

            BaseTypes = source;
        }
    }
}
