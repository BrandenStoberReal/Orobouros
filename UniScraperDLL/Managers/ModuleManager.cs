using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UniScraperDLL.Managers
{
    public static class ModuleManager
    {
        public static List<object> modules = new List<object>();

        /// <summary>
        /// Ensures the modules folder exists. This acts as a default modules path.
        /// </summary>
        public static void VerifyModulesFolderIntegrity()
        {
            // Create modules directory if non-existent
            if (!Directory.Exists("./modules"))
            {
                Directory.CreateDirectory("./modules");
            }
        }

        /// <summary>
        /// Loads assemblies. Can optionally provide a custom folder to load modules from.
        /// </summary>
        /// <param name="folder"></param>
        public static void LoadAssemblies(string? folder = null)
        {
            foreach (var mod in Directory.GetFiles(folder != null ? folder : "./modules"))
            {
                var DLL = Assembly.LoadFile(mod);
                modules.Add(DLL);
            }
        }
    }
}