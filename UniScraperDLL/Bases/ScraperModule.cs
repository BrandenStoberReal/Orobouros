using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static UniScraperDLL.UniAssemblyInfo;

namespace UniScraperDLL.Bases
{
    public class ScraperModule : ScraperInfo
    {
        public Assembly Module { get; set; }
    }
}