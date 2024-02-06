using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UniScraperDLL.UniAssemblyInfo;

namespace UniScraperDLL.Bases
{
    public class ScraperModuleData
    {
        public ScraperModule Module { get; set; }

        public ScraperContent ContentType { get; set; }

        public List<object> Content { get; set; } = new List<object>();

        public int RequestedDataAmount { get; set; } = 0;

        public string Website { get; set; } = String.Empty;
    }
}