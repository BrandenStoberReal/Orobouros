using FlyingSubmarineDLL.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Orobouros.Bases;
using static Orobouros.UniAssemblyInfo;
using Orobouros.Attributes;
using Orobouros.Tools;

namespace Orobouros.Managers
{
    public static class ModuleManager
    {
        /// <summary>
        /// A list of all loaded modules, casted to a class
        /// </summary>
        public static ModuleContainer Container = new ModuleContainer("ModuleManager Module Container", "Primary container for holding loaded Orobouros modules.");

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
            foreach (var mod in Directory.GetFiles(folder != null ? folder : Path.GetFullPath("./modules")))
            {
                // Only attempt to load valid DLL files, obviously
                if (mod.EndsWith(".dll") && NetAssemblyManager.IsDotNetAssembly(mod))
                {
                    DebugManager.WriteToDebugLog($"DLL found: {Path.GetFileName(mod)}");
                    Assembly DLL = Assembly.LoadFrom(mod); // Load DLL
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
                            Bases.Module module = new Bases.Module();

                            DebugManager.WriteToDebugLog($"Building ScraperModule...");
                            try
                            {
                                // Prevent further processing if module does not contain proper information.
                                if (moduleName == null || moduleDescription == null || moduleVersion == null || moduleSupportedSites == null || moduleSupportedContent == null || moduleSupportedSites == null || moduleSupportedContent == null)
                                {
                                    DebugManager.WriteToDebugLog($"WARNING: Module \"{Path.GetFileName(mod)}\" has malformed information and has been skipped. Please report this to the module's author.");
                                    continue;
                                }

                                // Fetch initial values
                                string ModName = (string)ReflectionManager.GetValueOfProperty(moduleName, psuedoAttribute);
                                string ModDesc = (string)ReflectionManager.GetValueOfProperty(moduleDescription, psuedoAttribute);
                                string ModVersion = (string)ReflectionManager.GetValueOfProperty(moduleVersion, psuedoAttribute);
                                List<string> ModWebsites = (List<string>)ReflectionManager.GetValueOfProperty(moduleSupportedSites, psuedoClass);
                                List<ModuleContent> ModContents = (List<ModuleContent>)ReflectionManager.GetValueOfProperty(moduleSupportedContent, psuedoClass);

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
                                        module.InitMethod = method;
                                    }
                                    if (ReflectionManager.MethodHasAttribute(method, typeof(ModuleScrape)))
                                    {
                                        module.ScrapeMethod = method;
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
    }
}