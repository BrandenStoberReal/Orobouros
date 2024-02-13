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

namespace Orobouros.Managers
{
    public static class ModuleManager
    {
        /// <summary>
        /// A list of all loaded modules, casted to a class
        /// </summary>
        public static List<Bases.Module> scraperModules = new List<Bases.Module>();

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
                    foreach (Type t in types)
                    {
                        // Fancy debug statement
                        DebugManager.WriteToDebugLog($"Enumerating type \"{t.Name}\"...");

                        // Fetch the type's TypeInfo
                        TypeInfo tInfo = t.GetTypeInfo();

                        // Fetch attributes for the type to determine if its flagged as the main class
                        object[] attributes = tInfo.GetCustomAttributes(true);
                        DebugManager.WriteToDebugLog($"Found {attributes.Length} custom attributes!");
                        if (attributes.Any(x => x.GetType().Name == typeof(OrobourosModule).Name))
                        {
                            // Fetch the attribute type for the main module class
                            object? rawInfoAttribute = attributes.FirstOrDefault(x => x.GetType().Name == typeof(OrobourosModule).Name);
                            Type moduleInfoAttribute = rawInfoAttribute.GetType();

                            // Change boolean due to finding main class
                            mainClassFound = true;

                            // Cast to a psuedo-class for property fetching
                            object? psuedoClass = Activator.CreateInstance(t);
                            object? psuedoAttribute = rawInfoAttribute;

                            // Fancy debugging statements
                            DebugManager.WriteToDebugLog($"Main DLL class found: {t.Name} | {t.Namespace}");
                            DebugManager.WriteToDebugLog($"Nested Types: {t.GetNestedTypes().Length}");
                            DebugManager.WriteToDebugLog($"Fields: {t.GetFields().Length}");
                            DebugManager.WriteToDebugLog($"Properties: {t.GetProperties().Length}");
                            DebugManager.WriteToDebugLog($"Public Methods: {t.GetMethods(BindingFlags.Instance | BindingFlags.Public).Length}");

                            // Fetch fields
                            PropertyInfo? moduleName = moduleInfoAttribute.GetProperty("ModuleName");
                            PropertyInfo? moduleDescription = moduleInfoAttribute.GetProperty("ModuleDescription");
                            PropertyInfo? moduleVersion = moduleInfoAttribute.GetProperty("ModuleVersion");

                            // Placeholder attribute fields
                            PropertyInfo? moduleSupportedSites = null;
                            PropertyInfo? moduleSupportedContent = null;

                            // Fill placeholder fields
                            foreach (PropertyInfo prop in t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                            {
                                DebugManager.WriteToDebugLog($"Scanning Property: " + prop.Name);
                                object[] attrs = prop.GetCustomAttributes(true);
                                if (attrs.Any(x => x.GetType().Name == typeof(ModuleSites).Name))
                                {
                                    moduleSupportedSites = prop;
                                }
                                if (attrs.Any(x => x.GetType().Name == typeof(ModuleContents).Name))
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

                                // Debug module importing
                                DebugManager.WriteToDebugLog($"Name: {(string)moduleName.GetValue(psuedoAttribute, null)}");
                                DebugManager.WriteToDebugLog($"Description: {(string)moduleDescription.GetValue(psuedoAttribute, null)}");
                                DebugManager.WriteToDebugLog($"Version: {(string)moduleVersion.GetValue(psuedoAttribute, null)}");
                                DebugManager.WriteToDebugLog($"Supported Sites Count: {((List<string>)moduleSupportedSites.GetValue(psuedoClass, null)).Count}");
                                DebugManager.WriteToDebugLog($"Supported Content Count: {((List<ModuleContent>)moduleSupportedContent.GetValue(psuedoClass, null)).Count}");

                                // Assign values
                                module.ModuleAsm = DLL;

                                // Assign attribute-based values
                                module.Name = (string)moduleName.GetValue(psuedoAttribute, null);
                                module.Description = (string)moduleDescription.GetValue(psuedoAttribute, null);
                                module.Version = (string)moduleVersion.GetValue(psuedoAttribute, null);

                                // Assign statically-based values
                                module.SupportedWebsites = (List<string>)moduleSupportedSites.GetValue(psuedoClass, null);
                                module.SupportedContent = (List<ModuleContent>)moduleSupportedContent.GetValue(psuedoClass, null);
                                module.PsuedoClass = psuedoClass;
                                module.PsuedoAttribute = psuedoAttribute;

                                // Method scrapings
                                foreach (MethodInfo method in t.GetMethods(BindingFlags.Instance | BindingFlags.Public))
                                {
                                    DebugManager.WriteToDebugLog($"Scanning Method: {method.Name}");
                                    object[] attrs = method.GetCustomAttributes(true);
                                    if (attrs.Any(x => x.GetType().Name == typeof(ModuleInit).Name))
                                    {
                                        module.InitMethod = method;
                                    }
                                    if (attrs.Any(x => x.GetType().Name == typeof(ModuleScrape).Name))
                                    {
                                        module.ScrapeMethod = method;
                                    }
                                    if (attrs.Any(x => x.GetType().Name == typeof(ModuleSupplementary).Name))
                                    {
                                        module.SupplementaryMethods.Add(method);
                                    }
                                }

                                // Push module to the array
                                scraperModules.Add(module);
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