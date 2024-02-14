using Orobouros.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = Orobouros.Bases.Module;

namespace Orobouros.Managers
{
    public static class ScrapingManager
    {
        /// <summary>
        /// Token to cancel supplementary tasks
        /// </summary>
        public static CancellationToken SupplementaryCancelToken { get; set; }

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
                SupplementaryCancelToken = CancellationTokenSource.Token;

                while (true)
                {
                    // Handle cancellations
                    if (SupplementaryCancelToken.IsCancellationRequested)
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
    }
}