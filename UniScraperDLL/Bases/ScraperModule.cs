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
        /// <summary>
        /// Instantiated primary module class. This class represents the module's exported
        /// information class and contains all exported methods and code.
        /// </summary>
        public object? PsuedoClass { get; set; }

        /// <summary>
        /// Raw assembly of the module.
        /// </summary>
        public Assembly? Module { get; set; }

        /// <summary>
        /// Module's initialization method, if the author programmed one.
        /// </summary>
        public MethodInfo? InitMethod { get; set; }

        /// <summary>
        /// Module's scrape method. Must exist and has parameter restrictions.
        /// </summary>
        public MethodInfo ScrapeMethod { get; set; }
    }
}