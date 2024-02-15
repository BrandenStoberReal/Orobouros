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
        /// <summary>
        /// URL to scrape data from
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Types of data to scrape from URL
        /// </summary>
        public List<ModuleContent> RequestedContent { get; set; } = new List<ModuleContent>();

        /// <summary>
        /// How many instances of the specified scraping data to scrape. This is only really used by
        /// modules which use the Subposts content type.
        /// </summary>
        public int ScrapeInstances { get; set; }
    }
}