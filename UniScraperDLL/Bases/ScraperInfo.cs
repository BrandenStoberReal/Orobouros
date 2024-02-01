using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UniScraperDLL.UniAssemblyInfo;

namespace UniScraperDLL.Bases
{
    public class ScraperInfo
    {
        public virtual string Name { get; set; } = String.Empty;
        public virtual string Description { get; set; } = String.Empty;
        public virtual string ModuleVersion { get; set; } = String.Empty;
        public virtual List<string> SupportedWebsites { get; set; } = new List<string>();
        public virtual List<ScraperContent> SupportedContent { get; set; } = new List<ScraperContent>();
    }
}