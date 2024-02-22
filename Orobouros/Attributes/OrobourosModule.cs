using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingSubmarineDLL.Attributes
{
    public class OrobourosModule : Attribute
    {
        public string ModuleName { get; private set; }
        public string ModuleDescription { get; private set; }
        public string ModuleVersion { get; private set; }
        public string ModuleGUID { get; private set; }

        public OrobourosModule(string moduleName, string guid, string moduleDescription = "Default Description", string moduleVersion = "1.0.0")
        {
            ModuleName = moduleName;
            ModuleDescription = moduleDescription;
            ModuleVersion = moduleVersion;
            ModuleGUID = guid;
        }
    }
}