using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingSubmarineDLL.Attributes
{
    public class FlyingSubmarineModule : Attribute
    {
        public string ModuleName { get; private set; }
        public string ModuleDescription { get; private set; }
        public string ModuleVersion { get; private set; }

        public FlyingSubmarineModule(string moduleName, string moduleDescription, string moduleVersion)
        {
            ModuleName = moduleName;
            ModuleDescription = moduleDescription;
            ModuleVersion = moduleVersion;
        }
    }
}