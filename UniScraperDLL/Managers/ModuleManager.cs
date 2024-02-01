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
        /// A list of all reflected module main classes as types.
        /// </summary>
        public static List<Type> scraperModuleTypes = new List<Type>();

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
            foreach (var mod in Directory.GetFiles(folder != null ? folder : "./modules"))
            {
                if (mod.EndsWith(".dll"))
                {
                    Assembly DLL = Assembly.LoadFile(mod);
                    Type[] types = DLL.GetTypes();

                    // Scan for the scraper information class
                    foreach (Type t in types)
                    {
                        if (t.IsClass && t.GetField("ModuleVersion") != null)
                        {
                            scraperModuleTypes.Add(t);

                            // Fetch fields
                            FieldInfo? moduleName = t.GetField("Name");
                            FieldInfo? moduleDescription = t.GetField("Description");
                            FieldInfo? moduleVersion = t.GetField("ModuleVersion");
                            FieldInfo? moduleSupportedSites = t.GetField("SupportedWebsites");
                            FieldInfo? moduleSupportedContent = t.GetField("SupportedContent");

                            // Initiate module
                            ScraperModule module = new ScraperModule();

                            // Assign values
                            module.Module = DLL;
                            module.Name = (string)moduleName.GetValue(null);
                            module.Description = (string)moduleDescription.GetValue(null);
                            module.ModuleVersion = (string)moduleVersion.GetValue(null);
                            module.SupportedWebsites = (List<string>)moduleSupportedSites.GetValue(null);
                            module.SupportedContent = (List<ScraperContent>)moduleSupportedContent.GetValue(null);
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