using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniScraperDLL.Bases;
using UniScraperDLL.Managers;

namespace UniScraperDLLTests.DLL
{
    [TestClass]
    public class ModuleTests
    {
        [TestMethod(displayName: "Module Handler - Import Modules")]
        public void Import_Modules()
        {
            ModuleManager.LoadAssemblies();
            foreach (ScraperModule module in ModuleManager.scraperModules)
            {
                System.Diagnostics.Trace.WriteLine($"{module.Name} | {module.ModuleVersion}");
            }
            Assert.IsTrue(ModuleManager.scraperModules.Count > 0);
        }
    }
}