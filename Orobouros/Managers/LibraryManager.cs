using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Managers
{
    public static class LibraryManager
    {
        /// <summary>
        /// Ensures the library folder is created and ready.
        /// </summary>
        public static void VerifyLibraryFolder()
        {
            if (!Directory.Exists("./libraries"))
            {
                Directory.CreateDirectory("./libraries");
            }
        }

        /// <summary>
        /// Loads assemblies from a specified folder.
        /// </summary>
        /// <param name="folder"></param>
        public static void LoadLibraries(string folder = "./libraries")
        {
            VerifyLibraryFolder();
            foreach (string file in Directory.GetFiles(folder))
            {
                // Load dependencies
                if (file.EndsWith(".dll") && NetAssemblyManager.IsDotNetAssembly(file))
                {
                    Assembly assembly = Assembly.LoadFrom(file);
                    if (AppDomain.CurrentDomain.GetAssemblies().Any(x => x.GetName() == assembly.GetName()))
                    {
                        // Ensure we dont load dependencies twice.
                        continue;
                    }
                    AppDomain.CurrentDomain.Load(assembly.GetName());
                    DebugManager.WriteToDebugLog($"[+] Assembly \"{assembly.GetName().Name}\" loaded into current appdomain!");
                }
            }
        }

        /// <summary>
        /// Loads referenced libraries inside DLLs. Typically doesn't work unless the module bundles
        /// their libraries strangely.
        /// </summary>
        /// <param name="assembly"></param>
        public static void LoadReferencedAssemblies(Assembly assembly)
        {
            foreach (AssemblyName name in assembly.GetReferencedAssemblies())
            {
                AppDomain.CurrentDomain.Load(name);
            }
        }
    }
}