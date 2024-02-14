using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orobouros.Bases;
using Orobouros.Managers;

namespace OrobourosTests.DLL
{
    [TestClass]
    public class ModuleTests
    {
        [TestMethod(displayName: "Module Handler - Import Modules")]
        public void Import_Modules()
        {
            ModuleManager.LoadAssemblies();
            foreach (Module module in ModuleManager.LoadedOrobourosModules)
            {
                System.Diagnostics.Trace.WriteLine($"{module.Name} | {module.Version}");
            }
            Assert.IsTrue(ModuleManager.LoadedOrobourosModules.Count > 0);
        }
    }
}