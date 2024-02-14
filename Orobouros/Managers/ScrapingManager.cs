using Orobouros.Attributes;
using Orobouros.Bases;
using System;
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
                            method.Invoke(mod.PsuedoClass, null);
                        }
                    }
                }
            }).Start();
        }

        /// <summary>
        /// Performs a scrape request against all loaded modules.
        /// </summary>
        /// <param name="baseDomain"></param>
        /// <param name="url"></param>
        /// <param name="contentToFetch"></param>
        /// <param name="numberOfObjectsToFetch"></param>
        /// <returns></returns>
        public static ModuleData? ScrapeURL(string baseDomain, string url, List<ModuleContent> contentToFetch, int numberOfObjectsToFetch)
        {
            Module? usedModule = null;

            // Find module with supported website
            foreach (Module mod in ModuleManager.LoadedOrobourosModules)
            {
                foreach (string Website in mod.SupportedWebsites)
                {
                    if (Website.Contains(baseDomain))
                    {
                        usedModule = mod;
                    }
                }
            }

            if (usedModule == null)
            {
                DebugManager.WriteToDebugLog($"No module found with URL \"{url}\"! Ensure you have a supported module installed.");
                return null;
            }
            else
            {
                // Check if module supports content we want
                bool invalidContent = false;
                foreach (ModuleContent content in contentToFetch)
                {
                    if (!usedModule.SupportedContent.Contains(content))
                    {
                        invalidContent = true;
                    }
                }

                // Bad content was requested
                if (invalidContent)
                {
                    DebugManager.WriteToDebugLog($"Content has been requested that the discovered module does not support! Please ensure you have the correct module installed.");
                    return null;
                }

                // Generate parameters to inject
                ScrapeParameters parms = new ScrapeParameters();
                parms.URL = url;
                parms.RequestedContent = parms.RequestedContent.Concat(contentToFetch).ToList();
                parms.RequestedData = numberOfObjectsToFetch;

                // Run method & cast return
                return (ModuleData)usedModule.ScrapeMethod.Invoke(usedModule.PsuedoClass, new object[] { parms });
            }
        }
    }
}