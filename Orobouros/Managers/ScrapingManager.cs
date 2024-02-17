using Orobouros.Attributes;
using Orobouros.Bases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Orobouros.UniAssemblyInfo;
using Module = Orobouros.Bases.Module;

namespace Orobouros.Managers
{
    public static class ScrapingManager
    {
        /// <summary>
        /// Token to cancel supplementary tasks. Tasks should be cancelled on application shutdown.
        /// </summary>
        public static CancellationTokenSource SupplementaryCancelToken { get; set; }

        /// <summary> Initializes scraper modules & runs startup logic. ONLY CALL THIS ONCE UNLESS
        /// YOU KNOW WHAT YOU'RE DOING!</summary> <param name="modulesPath">Optional custom modules
        /// folder to load</param>
        public static void InitializeModules(string modulesPath = "./modules")
        {
            // Load assemblies
            ModuleManager.LoadAssemblies(modulesPath);

            // Start supplementary methods
            new Thread(() =>
            {
                // Run thread in background (obviously)
                Thread.CurrentThread.IsBackground = true;
                Thread.CurrentThread.Name = "Orobouros Supplementary Methods Worker";

                // Create new token source
                CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

                // Assign new token to parent class
                SupplementaryCancelToken = CancellationTokenSource;

                while (true)
                {
                    // Handle cancellations
                    if (SupplementaryCancelToken.Token.IsCancellationRequested)
                    {
                        break;
                    }

                    // Run all supplementary methods
                    foreach (Module mod in ModuleManager.LoadedOrobourosModules)
                    {
                        foreach (MethodInfo method in mod.SupplementaryMethods)
                        {
                            ReflectionManager.InvokeReflectedMethod(method, mod.PsuedoClass);
                        }
                    }
                }
            }).Start();
        }

        /// <summary>
        /// Performs a scrape request against all loaded modules.
        /// </summary>
        /// <param name="url">
        /// Specific URL to scrape. This should be a URL that points to a page with scrapable data.
        /// </param>
        /// <param name="contentToFetch">
        /// List of content to fetch from supported modules. Keep in mind the module may not support
        /// the data types you request.
        /// </param>
        /// <param name="numofinstances">
        /// (Optional) Number of instances to scrape. Defaults to 1 and is rarely changed.
        /// </param>
        /// <returns></returns>
        public static ModuleData? ScrapeURL(string url, List<ModuleContent> contentToFetch, int numofinstances = 1)
        {
            // Placeholder for discovered module
            List<Module> foundModules = new List<Module>();

            // Fetch base domain for compatibility checks
            Uri myUri = new Uri(url);
            string baseDomain = myUri.Host;

            // Find module with supported website
            foreach (Module mod in ModuleManager.LoadedOrobourosModules)
            {
                foreach (string Website in mod.SupportedWebsites)
                {
                    if (Website.Contains(baseDomain))
                    {
                        foundModules.Add(mod);
                    }
                }
            }

            if (foundModules == null)
            {
                // Compatible module not found
                DebugManager.WriteToDebugLog($"No module found with URL \"{url}\"! Ensure you have a supported module installed.");
                return null;
            }
            else
            {
                // Check if module supports content we want
                foreach (ModuleContent content in contentToFetch)
                {
                    foreach (Module mod in foundModules)
                    {
                        if (!mod.SupportedContent.Contains(content))
                        {
                            foundModules.Remove(mod);
                        }
                    }
                }

                // Bad content was requested
                if (foundModules.Count == 0)
                {
                    DebugManager.WriteToDebugLog($"Content has been requested that the discovered module(s) do not support! Please ensure you have the correct module(s) installed.");
                    return null;
                }

                // Generate parameters to inject
                ScrapeParameters parms = new ScrapeParameters();
                parms.URL = url;
                parms.RequestedContent = parms.RequestedContent.Concat(contentToFetch).ToList();
                parms.ScrapeInstances = numofinstances;

                // Multiple supported modules found
                if (foundModules.Count > 1)
                {
                    DebugManager.WriteToDebugLog($"Multiple modules for the same website supporting the same content found. A random one will be selected. Please avoid this behavior in the future.");
                    Random rng = new Random();
                    int randInt = rng.Next(foundModules.Count);
                    return (ModuleData?)ReflectionManager.InvokeReflectedMethod(foundModules[randInt].ScrapeMethod, foundModules[randInt].PsuedoClass, new object[] { parms });
                }
                else
                {
                    // Only 1 module found, should be default behavior
                    return (ModuleData?)ReflectionManager.InvokeReflectedMethod(foundModules.FirstOrDefault().ScrapeMethod, foundModules.FirstOrDefault().PsuedoClass, new object[] { parms });
                }
            }
        }
    }
}