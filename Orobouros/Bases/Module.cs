using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Orobouros.UniAssemblyInfo;

namespace Orobouros.Bases
{
    public class Module
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }

        /// <summary>
        /// Instantiated primary module class. This class represents the module's exported
        /// information class and contains all exported methods and code.
        /// </summary>
        public object? PsuedoClass { get; set; }

        /// <summary>
        /// Instantiated primary module attribute. This is attached to the main module class and
        /// provides module information.
        /// </summary>
        public object? PsuedoAttribute { get; set; }

        /// <summary>
        /// List of content supported by the module.
        /// </summary>
        public List<ModuleContent> SupportedContent { get; set; } = new List<ModuleContent>();

        /// <summary>
        /// List of websites supported by the module.
        /// </summary>
        public List<string> SupportedWebsites { get; set; } = new List<string>();

        /// <summary>
        /// Raw assembly of the module.
        /// </summary>
        public Assembly? ModuleAsm { get; set; }

        /// <summary>
        /// Module's initialization method, if the author programmed one.
        /// </summary>
        public MethodInfo? InitMethod { get; set; }

        /// <summary>
        /// Module's scrape method. Must exist and has parameter restrictions.
        /// </summary>
        public MethodInfo ScrapeMethod { get; set; }

        /// <summary>
        /// </summary>
        public List<MethodInfo> SupplementaryMethods { get; set; } = new List<MethodInfo>();
    }
}