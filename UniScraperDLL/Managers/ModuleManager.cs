using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UniScraperDLL.Bases;
using static UniScraperDLL.UniAssemblyInfo;

namespace UniScraperDLL.Managers
{
    public static class ModuleManager
    {
        /// <summary>
        /// A list of all loaded modules, casted to a class
        /// </summary>
        public static List<ScraperModule> scraperModules = new List<ScraperModule>();

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
            foreach (var mod in Directory.GetFiles(folder != null ? folder : Path.GetFullPath("./modules")))
            {
                // Only attempt to load valid DLL files, obviously
                if (mod.EndsWith(".dll") && NetAssemblyManager.IsDotNetAssembly(mod))
                {
                    System.Diagnostics.Trace.WriteLine($"DLL found: {Path.GetFileName(mod)}");
                    Assembly DLL = Assembly.LoadFrom(mod); // Load DLL
                    Type[] types = DLL.GetTypes(); // Fetch types so we can parse them below

                    // Scan for the scraper information class
                    foreach (Type t in types)
                    {
                        // Fancy debug statement
                        System.Diagnostics.Trace.WriteLine($"Enumerating type \"{t.Name}\"...");

                        // Ensure the class is the main module by finding the version number
                        if (t.IsClass && t.GetProperty("ModuleVersion") != null)
                        {
                            // Cast to a psuedo-class for property fetching
                            object? psuedoClass = Activator.CreateInstance(t);

                            // Fancy debugging statements
                            System.Diagnostics.Trace.WriteLine($"Main DLL class found: {t.Name} | {t.Namespace}");
                            System.Diagnostics.Trace.WriteLine($"Nested Types: {t.GetNestedTypes().Length}");
                            System.Diagnostics.Trace.WriteLine($"Fields: {t.GetFields().Length}");
                            System.Diagnostics.Trace.WriteLine($"Properties: {t.GetProperties().Length}");
                            System.Diagnostics.Trace.WriteLine($"Public Methods: {t.GetMethods(BindingFlags.Public).Length}");

                            // Fetch fields
                            PropertyInfo? moduleName = t.GetProperty("Name");
                            PropertyInfo? moduleDescription = t.GetProperty("Description");
                            PropertyInfo? moduleVersion = t.GetProperty("ModuleVersion");
                            PropertyInfo? moduleSupportedSites = t.GetProperty("SupportedWebsites");
                            PropertyInfo? moduleSupportedContent = t.GetProperty("SupportedContent");

                            // Initiate module
                            ScraperModule module = new ScraperModule();

                            System.Diagnostics.Trace.WriteLine($"Building ScraperModule...");
                            try
                            {
                                // Debug module importing
                                System.Diagnostics.Trace.WriteLine($"Name: {(string)moduleName.GetValue(psuedoClass, null)}");
                                System.Diagnostics.Trace.WriteLine($"Description: {(string)moduleDescription.GetValue(psuedoClass, null)}");
                                System.Diagnostics.Trace.WriteLine($"Version: {(string)moduleVersion.GetValue(psuedoClass, null)}");
                                System.Diagnostics.Trace.WriteLine($"Supported Sites Count: {((List<string>)moduleSupportedSites.GetValue(psuedoClass, null)).Count}");
                                System.Diagnostics.Trace.WriteLine($"Supported Content Count: {((List<ScraperContent>)moduleSupportedContent.GetValue(psuedoClass, null)).Count}");

                                // Assign values
                                module.Module = DLL;
                                module.Name = (string)moduleName.GetValue(psuedoClass, null);
                                module.Description = (string)moduleDescription.GetValue(psuedoClass, null);
                                module.ModuleVersion = (string)moduleVersion.GetValue(psuedoClass, null);
                                module.SupportedWebsites = (List<string>)moduleSupportedSites.GetValue(psuedoClass, null);
                                module.SupportedContent = (List<ScraperContent>)moduleSupportedContent.GetValue(psuedoClass, null);
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Trace.WriteLine($"FATAL: {ex.Message}");
                            }

                            // Push module to the array
                            scraperModules.Add(module);
                        }
                    }
                }
                else
                {
                    File.Delete(mod);
                }
            }
        }
    }
}