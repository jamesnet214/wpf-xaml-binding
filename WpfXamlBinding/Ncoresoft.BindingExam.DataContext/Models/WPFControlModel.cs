using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncoresoft.BindingExam.DataContext.Models
{
    public class WPFControlModel
    {
        public Type Control { get; set; }
        public List<WPFControlModel> Controls { get; set; }
    }
}
