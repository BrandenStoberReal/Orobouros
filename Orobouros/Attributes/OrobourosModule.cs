using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingSubmarineDLL.Attributes
{
    public class OrobourosModule : Attribute
    {
        /// <summary>
        /// Self explanatory. REQUIRED.
        /// </summary>
        public string ModuleName { get; private set; }

        /// <summary>
        /// Self explanatory. OPTIONAL.
        /// </summary>
        public string ModuleDescription { get; private set; }

        /// <summary>
        /// Self explanatory. OPTIONAL.
        /// </summary>
        public string ModuleVersion { get; private set; }

        /// <summary>
        /// Self explanatory. REQUIRED.
        /// </summary>
        public string ModuleGUID { get; private set; }

        /// <summary>
        /// Attribute constructor.
        /// </summary>
        /// <param name="moduleName">Human-readable module name (Required)</param>
        /// <param name="guid">Module unique GUID (Required)</param>
        /// <param name="moduleDescription">Human-readable module description (Optional)</param>
        /// <param name="moduleVersion">Module version (Optional)</param>
        public OrobourosModule(string moduleName, string guid, string moduleDescription = "Default Description", string moduleVersion = "1.0.0")
        {
            ModuleName = moduleName;
            ModuleDescription = moduleDescription;
            ModuleVersion = moduleVersion;
            ModuleGUID = guid;
        }
    }
}