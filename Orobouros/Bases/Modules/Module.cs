﻿using Orobouros.Tools.Web;
using System.Reflection;
using static Orobouros.Orobouros;

namespace Orobouros.Bases
{
    /// <summary>
    /// Instantiated module class. Represents a loaded assembly from disk.
    /// </summary>
    public class Module
    {
        /// <summary>
        /// Module name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Module description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Module version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Unique identifier for the module. Used primarily for the database system.
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Module database file path.
        /// </summary>
        public string DatabaseFile { get; set; }

        /// <summary>
        /// Module protobuf cache path.
        /// </summary>
        public string ProtobufDirectory { get; set; }

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
        public List<ContentType> SupportedContent { get; set; } = new List<ContentType>();

        /// <summary>
        /// List of websites supported by the module.
        /// </summary>
        public List<string> SupportedWebsites { get; set; } = new List<string>();

        /// <summary>
        /// List of posts fetched from the protobuf cache.
        /// </summary>
        public List<Post> CachedPosts { get; set; } = new List<Post>();

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