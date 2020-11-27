using NcoreExam.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcoreExam.DataContextTest.Models
{
    public class BaseClassModel : ObservableObject
    {
        private List<BaseClassModel> _baseTypes;
        public List<BaseClassModel> BaseTypes
        {
            get { return _baseTypes; }
            set { _baseTypes = value; OnPropertyChanged(); }

        }

        private Type _type;
        public Type Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }
    }
}
