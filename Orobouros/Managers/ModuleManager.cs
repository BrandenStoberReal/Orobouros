using Orobouros.Attributes;
using Orobouros.Bases;
using Orobouros.Tools.Containers;
using System.Reflection;
using static Orobouros.UniAssemblyInfo;
using Module = Orobouros.Bases.Module;

namespace Orobouros.Managers
{
    /// <summary>
    /// Manages everything related to modules and allows the framework to interpret the module's data.
    /// </summary>
    public static class ModuleManager
    {
        /// <summary>
        /// A list of all loaded modules, casted to a class
        /// </summary>
        public static ModuleContainer Container { get; private set; } = new ModuleContainer("ModuleManager Module Container", "Primary container for holding loaded Orobouros modules.");

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
        /// Loads module assemblies. Can optionally provide a custom folder to load modules from.
        /// </summary>
        /// <param name="folder">Folder path that contains modules to load.</param>
        /// <param name="aggressive">Whether to aggressively delete non-module files in the directory.</param>
        public static void LoadAssemblies(string? folder = null, bool aggressive = false)
        {
            Container.Modules.Clear();
            VerifyModulesFolderIntegrity();
            LibraryManager.LoadLibraries();
            foreach (var mod in Directory.GetFiles(folder != null ? folder : Path.GetFullPath("./modules")))
            {
                // Only attempt to load valid DLL files, obviously
                if (mod.EndsWith(".dll") && NetAssemblyManager.IsDotNetAssembly(mod))
                {
                    DebugManager.WriteToDebugLog($"DLL found: {Path.GetFileName(mod)}");
                    Assembly DLL = RawAssemblyManager.LoadDLL(AssemblyLoadType.ByteStream, mod); // Load DLL
                    Type[] types = DLL.GetTypes(); // Fetch types so we can parse them below
                    bool mainClassFound = false;

                    // Enumerate through all types in the imported assembly
                    foreach (Type type in types)
                    {
                        // Fancy debug statement
                        DebugManager.WriteToDebugLog($"Enumerating type \"{type.Name}\"...");

                        // Determine if class has the attribute we need
                        if (ReflectionManager.TypeHasAttribute(type, typeof(OrobourosModule)))
                        {
                            // Load libraries
                            LibraryManager.LoadReferencedAssemblies(DLL);

                            // Fetch the attribute type for the main module class
                            object? rawInfoAttribute = ReflectionManager.FetchAttributeFromType(type, typeof(OrobourosModule));
                            Type moduleInfoAttribute = rawInfoAttribute.GetType();

                            // Change boolean due to finding main class
                            mainClassFound = true;

                            // Cast to a psuedo-class for property fetching
                            object? psuedoClass = ReflectionManager.CreateSkeletonClass(type);
                            object? psuedoAttribute = rawInfoAttribute;

                            // Fancy debugging statements
                            DebugManager.WriteToDebugLog($"Main DLL class found: {type.Name} | {type.Namespace}");
                            DebugManager.WriteToDebugLog($"Nested Types: {type.GetNestedTypes().Length}");
                            DebugManager.WriteToDebugLog($"Fields: {type.GetFields().Length}");
                            DebugManager.WriteToDebugLog($"Properties: {type.GetProperties().Length}");
                            DebugManager.WriteToDebugLog($"Public Methods: {type.GetMethods(BindingFlags.Instance | BindingFlags.Public).Length}");

                            // Fetch fields
                            PropertyInfo? moduleName = moduleInfoAttribute.GetProperty("ModuleName");
                            PropertyInfo? moduleDescription = moduleInfoAttribute.GetProperty("ModuleDescription");
                            PropertyInfo? moduleVersion = moduleInfoAttribute.GetProperty("ModuleVersion");
                            PropertyInfo? moduleGUID = moduleInfoAttribute.GetProperty("ModuleGUID");

                            // Placeholder attribute fields
                            PropertyInfo? moduleSupportedSites = null;
                            PropertyInfo? moduleSupportedContent = null;

                            // Fill placeholder fields
                            foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                            {
                                DebugManager.WriteToDebugLog($"Scanning Property: " + prop.Name);
                                if (ReflectionManager.PropertyHasAttribute(prop, typeof(ModuleSites)))
                                {
                                    moduleSupportedSites = prop;
                                }
                                if (ReflectionManager.PropertyHasAttribute(prop, typeof(ModuleContents)))
                                {
                                    moduleSupportedContent = prop;
                                }
                            }

                            // Initiate module
                            Module module = new Module();

                            DebugManager.WriteToDebugLog($"Building ScraperModule...");
                            try
                            {
                                // Prevent further processing if module does not contain proper information.
                                if (moduleName == null || moduleGUID == null || moduleSupportedSites == null || moduleSupportedContent == null || moduleSupportedSites == null || moduleSupportedContent == null)
                                {
                                    DebugManager.WriteToDebugLog($"WARNING: Module \"{Path.GetFileName(mod)}\" has malformed information and has been skipped. Please report this to the module's author.");
                                    continue;
                                }

                                // Fetch initial values
                                string? ModName = (string?)ReflectionManager.GetValueOfProperty(moduleName, psuedoAttribute);
                                string? ModDesc = (string?)ReflectionManager.GetValueOfProperty(moduleDescription, psuedoAttribute);
                                string? ModVersion = (string?)ReflectionManager.GetValueOfProperty(moduleVersion, psuedoAttribute);
                                string? ModGuid = (string?)ReflectionManager.GetValueOfProperty(moduleGUID, psuedoAttribute);
                                List<string>? ModWebsites = (List<string>?)ReflectionManager.GetValueOfProperty(moduleSupportedSites, psuedoClass);
                                List<ModuleContent>? ModContents = (List<ModuleContent>?)ReflectionManager.GetValueOfProperty(moduleSupportedContent, psuedoClass);

                                // Ensure modules with same GUID aren't loaded already
                                if (Container.Modules.Any(x => x.GUID == ModGuid))
                                {
                                    Module loadedMod = Container.Modules.First(x => x.GUID == ModGuid);
                                    DebugManager.WriteToDebugLog($"Module with GUID \"{ModGuid}\" has already been loaded! This means there are duplicate modules. This one has been skipped. Existing Module: {loadedMod.Name}");
                                    continue;
                                }

                                // Debug module importing
                                DebugManager.WriteToDebugLog($"Name: {ModName}");
                                DebugManager.WriteToDebugLog($"Description: {ModDesc}");
                                DebugManager.WriteToDebugLog($"Version: {ModVersion}");
                                DebugManager.WriteToDebugLog($"Supported Sites Count: {ModWebsites.Count}");
                                DebugManager.WriteToDebugLog($"Supported Content Count: {ModContents.Count}");

                                // Assign values
                                module.ModuleAsm = DLL;

                                // Assign attribute-based values
                                module.Name = ModName;
                                module.Description = ModDesc;
                                module.Version = ModVersion;
                                module.GUID = ModGuid;
                                module.DatabaseFile = DynamicDatabaseManager.FetchModuleDatabasePath(module);
                                DebugManager.WriteToDebugLog($"Database File: {module.DatabaseFile}");

                                // Assign statically-based values
                                module.SupportedWebsites = ModWebsites;
                                module.SupportedContent = ModContents;
                                module.PsuedoClass = psuedoClass;
                                module.PsuedoAttribute = psuedoAttribute;

                                // Method scrapings
                                foreach (MethodInfo method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public))
                                {
                                    DebugManager.WriteToDebugLog($"Scanning Method: {method.Name}");
                                    if (ReflectionManager.MethodHasAttribute(method, typeof(ModuleInit)))
                                    {
                                        if (module.InitMethod == null)
                                        {
                                            module.InitMethod = method;
                                        }
                                    }
                                    if (ReflectionManager.MethodHasAttribute(method, typeof(ModuleScrape)))
                                    {
                                        if (module.ScrapeMethod == null)
                                        {
                                            module.ScrapeMethod = method;
                                        }
                                    }
                                    if (ReflectionManager.MethodHasAttribute(method, typeof(ModuleSupplementary)))
                                    {
                                        module.SupplementaryMethods.Add(method);
                                    }
                                }

                                // Check for valid scrape method
                                if (module.ScrapeMethod == null)
                                {
                                    DebugManager.WriteToDebugLog($"ERROR: Module \"{module.Name}\" has no scrape method! This shouldn't happen. Please report this to the module's developer. This module will be skipped.");
                                    continue;
                                }

                                // Fetch return type
                                Type scrapeMethodReturnType = module.ScrapeMethod.ReturnType;

                                // Check for valid return type
                                if (scrapeMethodReturnType != typeof(ModuleData))
                                {
                                    DebugManager.WriteToDebugLog($"ERROR: Module \"{module.Name}\"'s scraper method does NOT return ModuleData! Please report this to the module's developer. This module will be skipped.");
                                    continue;
                                }

                                // Push module to the array
                                Container.Modules.Add(module);
                                DebugManager.WriteToDebugLog($"Methods: Invoking initializer method of module \"{module.Name}\" in a new thread!");

                                // Push module to appdomain
                                RawAssemblyManager.InsertAssemblyIntoMemory(DLL);

                                // Start module initializer thread
                                new Thread(() =>
                                {
                                    // Run thread in background (obviously)
                                    Thread.CurrentThread.IsBackground = true;
                                    if (module.InitMethod != null)
                                    {
                                        // Invoke the initializer on the psuedoclass
                                        module.InitMethod.Invoke(psuedoClass, null);
                                    }
                                }).Start();
                            }
                            catch (Exception ex)
                            {
                                // This means an error occurred loading module values and module
                                // processing cannot continue.
                                DebugManager.WriteToDebugLog($"FATAL: {ex.Message}");
                                DebugManager.WriteToDebugLog($"Module \"{Path.GetFileName(mod)}\" encountered an exception loading and has been skipped. Please report this to the module developer.");
                            }
                        }
                    }

                    if (!mainClassFound)
                    {
                        DebugManager.WriteToDebugLog($"WARNING: Module \"{Path.GetFileName(mod)}\" does not have a valid information class and has been skipped. Please report this to the module's author.");
                    }
                }
                else
                {
                    // Handle invalid files here
                    if (aggressive)
                    {
                        File.Delete(mod);
                    }
                }
            }
        }

        /// <summary>
        /// Unloads all loaded module assemblies. Note this keeps them loaded in the current appdomain.
        /// </summary>
        public static void UnloadAssemblies()
        {
            Container.Modules.Clear();
        }

        /// <summary>
        /// Fetches a module from the loaded list by its GUID.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>Module found, or null if no such module exists</returns>
        public static Module? FetchModule(string guid)
        {
            return Container.Modules.FirstOrDefault(x => x.GUID == guid);
        }

        /// <summary>
        /// Fetches a random loaded module.
        /// </summary>
        /// <returns></returns>
        public static Module FetchRandomModule()
        {
            Random rng = new Random();
            int index = rng.Next(0, Container.Modules.Count);
            return Container.Modules[index];
        }
    }
}