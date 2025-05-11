using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AITUC.ViewModels
{
    
        public class TabItemViewModel
        {
            public string Title { get; set; }
            public string ViewName { get; set; }
            public UserControl Content { get; set; }
        }
    
}
