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
                    System.Diagnostics.Trace.WriteLine($"DLL found: {Path.GetFileName(mod)}");
                    Assembly DLL = Assembly.LoadFrom(mod); // Load DLL
                    Type[] types = DLL.GetTypes(); // Fetch types so we can parse them below
                    bool mainClassFound = false;

                    // Enumerate through all types in the imported assembly
                    foreach (Type t in types)
                    {
                        // Fancy debug statement
                        System.Diagnostics.Trace.WriteLine($"Enumerating type \"{t.Name}\"...");

                        // Fetch the type's TypeInfo
                        TypeInfo tInfo = t.GetTypeInfo();

                        // Fetch attributes for the type to determine if its flagged as the main class
                        object[] attributes = tInfo.GetCustomAttributes(true);
                        System.Diagnostics.Trace.WriteLine($"Found {attributes.Length} custom attributes!");
                        if (attributes.Any(x => x.GetType().Name == typeof(OrobourosModule).Name))
                        {
                            // Fetch the attribute for the main module class
                            Type moduleInfoAttribute = attributes.FirstOrDefault(x => x.GetType().Name == typeof(OrobourosModule).Name).GetType();

                            // Change boolean due to finding main class
                            mainClassFound = true;

                            // Cast to a psuedo-class for property fetching
                            object? psuedoClass = Activator.CreateInstance(t);
                            object? psuedoAttribute = attributes.FirstOrDefault(x => x.GetType().Name == typeof(OrobourosModule).Name);

                            // Fancy debugging statements
                            System.Diagnostics.Trace.WriteLine($"Main DLL class found: {t.Name} | {t.Namespace}");
                            System.Diagnostics.Trace.WriteLine($"Nested Types: {t.GetNestedTypes().Length}");
                            System.Diagnostics.Trace.WriteLine($"Fields: {t.GetFields().Length}");
                            System.Diagnostics.Trace.WriteLine($"Properties: {t.GetProperties().Length}");
                            System.Diagnostics.Trace.WriteLine($"Public Methods: {t.GetMethods(BindingFlags.Instance | BindingFlags.Public).Length}");

                            // Fetch fields
                            PropertyInfo? moduleName = moduleInfoAttribute.GetProperty("ModuleName");
                            PropertyInfo? moduleDescription = moduleInfoAttribute.GetProperty("ModuleDescription");
                            PropertyInfo? moduleVersion = moduleInfoAttribute.GetProperty("ModuleVersion");
                            PropertyInfo? moduleSupportedSites = t.GetProperty("SupportedWebsites");
                            PropertyInfo? moduleSupportedContent = t.GetProperty("SupportedContent");

                            // Initiate module
                            Bases.Module module = new Bases.Module();

                            System.Diagnostics.Trace.WriteLine($"Building ScraperModule...");
                            try
                            {
                                // Prevent further processing if module does not contain proper information.
                                if (moduleName == null || moduleDescription == null || moduleVersion == null || moduleSupportedSites == null || moduleSupportedContent == null)
                                {
                                    System.Diagnostics.Trace.WriteLine($"WARNING: Module \"{Path.GetFileName(mod)}\" has malformed information and has been skipped. Please report this to the module's author.");
                                    continue;
                                }

                                // Debug module importing
                                System.Diagnostics.Trace.WriteLine($"Name: {(string)moduleName.GetValue(psuedoAttribute, null)}");
                                System.Diagnostics.Trace.WriteLine($"Description: {(string)moduleDescription.GetValue(psuedoAttribute, null)}");
                                System.Diagnostics.Trace.WriteLine($"Version: {(string)moduleVersion.GetValue(psuedoAttribute, null)}");
                                System.Diagnostics.Trace.WriteLine($"Supported Sites Count: {((List<string>)moduleSupportedSites.GetValue(psuedoClass, null)).Count}");
                                System.Diagnostics.Trace.WriteLine($"Supported Content Count: {((List<ModuleContent>)moduleSupportedContent.GetValue(psuedoClass, null)).Count}");

                                // Assign values
                                module.ModuleAsm = DLL;
                                module.Name = (string)moduleName.GetValue(psuedoAttribute, null);
                                module.Description = (string)moduleDescription.GetValue(psuedoAttribute, null);
                                module.Version = (string)moduleVersion.GetValue(psuedoAttribute, null);
                                module.SupportedWebsites = (List<string>)moduleSupportedSites.GetValue(psuedoClass, null);
                                module.SupportedContent = (List<ModuleContent>)moduleSupportedContent.GetValue(psuedoClass, null);
                                module.PsuedoClass = psuedoClass;

                                // Method scrapings
                                foreach (MethodInfo method in t.GetMethods(BindingFlags.Instance | BindingFlags.Public))
                                {
                                    System.Diagnostics.Trace.WriteLine($"Scanning Method: {method.Name}");
                                    object[] attrs = method.GetCustomAttributes(true);
                                    if (attrs.Any(x => x.GetType().Name == "ModuleInit"))
                                    {
                                        module.InitMethod = method;
                                    }
                                    if (attrs.Any(x => x.GetType().Name == "ModuleScrape"))
                                    {
                                        module.ScrapeMethod = method;
                                    }
                                    if (attrs.Any(x => x.GetType().Name == "ModuleSupplementary"))
                                    {
                                        module.SupplementaryMethods.Add(method);
                                    }
                                }

                                // Push module to the array
                                scraperModules.Add(module);
                                System.Diagnostics.Trace.WriteLine($"Methods: Invoking initializer method of module \"{module.Name}\" in a new thread!");

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
                                System.Diagnostics.Trace.WriteLine($"FATAL: {ex.Message}");
                                System.Diagnostics.Trace.WriteLine($"Module \"{Path.GetFileName(mod)}\" encountered an exception loading and has been skipped. Please report this to the module developer.");
                            }
                        }
                    }

                    if (!mainClassFound)
                    {
                        System.Diagnostics.Trace.WriteLine($"WARNING: Module \"{Path.GetFileName(mod)}\" does not have a valid information class and has been skipped. Please report this to the module's author.");
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