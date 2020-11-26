using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcoreExam.DataContextTest.Models
{
    public class ControlTypeModel
    {
        public ControlTypeModel(Type controlType)
        {
            ControlType = controlType;
        }

        public string Name { get { return ControlType.Name; } }
        public Type ControlType { get; set; }
    }
}
