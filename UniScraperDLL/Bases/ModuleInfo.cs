using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UniScraperDLL.UniAssemblyInfo;

namespace UniScraperDLL.Bases
{
    public class ModuleInfo
    {
        public virtual List<string> SupportedWebsites { get; set; } = new List<string>();
        public virtual List<ModuleContent> SupportedContent { get; set; } = new List<ModuleContent>();
    }
}