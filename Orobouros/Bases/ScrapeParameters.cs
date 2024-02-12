using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Orobouros.UniAssemblyInfo;

namespace Orobouros.Bases
{
    public class ScrapeParameters
    {
        public string URL { get; set; }
        public int RequestedData { get; set; }
        public List<ModuleContent> RequestedContent { get; set; } = new List<ModuleContent>();
    }
}