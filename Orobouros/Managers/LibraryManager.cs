using System.Reflection;

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
                LoggingManager.LogInformation("Libraries folder didn't exist! It has been created automatically.");
            }
        }

        /// <summary>
        /// Loads assemblies from a specified folder.
        /// </summary>
        /// <param name="folder"></param>
        public static void LoadLibraries(string folder = "./libraries")
        {
            LoggingManager.LogInformation($"Loading module libraries from folder \"{folder}\"...");
            VerifyLibraryFolder();
            foreach (string file in Directory.GetFiles(folder))
            {
                // Load dependencies
                if (file.EndsWith(".dll") && NetAssemblyManager.IsDotNetAssembly(file))
                {
                    Assembly assembly = RawAssemblyManager.LoadDLL(AssemblyLoadType.Direct, file);
                    if (AppDomain.CurrentDomain.GetAssemblies().Any(x => x.GetName() == assembly.GetName()))
                    {
                        // Ensure we dont load dependencies twice.
                        continue;
                    }
                    RawAssemblyManager.InsertAssemblyIntoMemory(assembly);
                    LoggingManager.WriteToDebugLog($"Assembly \"{assembly.GetName().Name}\" loaded into current appdomain!");
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
            LoggingManager.LogInformation($"Attempting to load referenced libraries from assembly \"{assembly.GetName().Name}\"...");
            foreach (AssemblyName name in assembly.GetReferencedAssemblies())
            {
                LoggingManager.WriteToDebugLog($"Reference Loader Found Declared Dependency: {name.Name}");
                RawAssemblyManager.InsertAssemblyNameIntoMemory(name);
            }
        }
    }
}