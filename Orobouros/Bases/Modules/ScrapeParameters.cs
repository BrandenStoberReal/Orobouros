﻿using Orobouros.Tools.Web;
using static Orobouros.UniAssemblyInfo;

namespace Orobouros
{
    /// <summary>
    /// Parameters passed to each module's scrape method.
    /// </summary>
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

        /// <summary>
        /// List of subposts that can be passed from a previous step.
        /// </summary>
        public List<Post> Subposts { get; set; }
    }
}