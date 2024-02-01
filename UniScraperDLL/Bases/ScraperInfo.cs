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
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string ModuleVersion { get; set; } = String.Empty;
        public List<string> SupportedWebsites { get; set; } = new List<string>();
        public List<ScraperContent> SupportedContent { get; set; } = new List<ScraperContent>();
    }
}