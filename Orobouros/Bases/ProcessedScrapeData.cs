using Orobouros.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Orobouros.UniAssemblyInfo;

namespace Orobouros.Bases
{
    public class ProcessedScrapeData
    {
        /// <summary>
        /// Assigned content type of the returned data payload.
        /// </summary>
        public ModuleContent ContentType { get; private set; }

        /// <summary>
        /// URL the data payload was scraped from.
        /// </summary>
        public string URL { get; private set; }

        /// <summary>
        /// Returned data payload from scraper module. Can be any class depending on how the module
        /// returns its data, and thus typecasting should be sanitized.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Human-friendly name of the data payload's class type. Useful in reflection for
        /// determining what class to cast the value to.
        /// </summary>
        public string ValueClassType { get; private set; }

        /// <summary>
        /// Processed scrape data constructor. Multiple instances of this class can have the same
        /// URL parameter depending on the type of content requested.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="url"></param>
        /// <param name="value"></param>
        public ProcessedScrapeData(ModuleContent type, string url, object value)
        {
            ContentType = type;
            URL = url;
            Value = value;
            ValueClassType = value.GetType().Name;
        }
    }
}