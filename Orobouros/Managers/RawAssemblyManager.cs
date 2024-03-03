using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Managers
{
    /// <summary>
    /// Enum representing the method to load assemblies.
    /// </summary>
    public enum AssemblyLoadType
    {
        /// <summary>
        /// Load the assembly directly, locking the file while the program is in use.
        /// </summary>
        Direct,

        /// <summary>
        /// Load the assembly via reading all bytes and loading directly from memory. Does not lock
        /// the file, but loses out on associated benefits with loading directly.
        /// </summary>
        ByteStream
    }

    public static class RawAssemblyManager
    {
        /// <summary>
        /// Instantiates the assembly class via the specified load method.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Assembly LoadDLL(AssemblyLoadType method, string path)
        {
            if (method == AssemblyLoadType.Direct)
            {
                return Assembly.LoadFrom(path);
            }
            else
            {
                return Assembly.Load(File.ReadAllBytes(path));
            }
        }

        /// <summary>
        /// Loads an assembly into the current AppDomain. May have unintended results.
        /// </summary>
        /// <param name="assembly"></param>
        public static void InsertAssemblyIntoMemory(Assembly assembly)
        {
            if (!AppDomain.CurrentDomain.GetAssemblies().Any(x => x.GetName() == assembly.GetName()))
            {
                // Load module into appdomain
                AppDomain.CurrentDomain.Load(assembly.GetName());
            }
        }

        /// <summary>
        /// Loads an AssemblyName into the current AppDomain. May have unintended results.
        /// </summary>
        /// <param name="assembly"></param>
        public static void InsertAssemblyNameIntoMemory(AssemblyName assembly)
        {
            if (!AppDomain.CurrentDomain.GetAssemblies().Any(x => x.GetName() == assembly))
            {
                // Load module into appdomain
                AppDomain.CurrentDomain.Load(assembly);
            }
        }
    }
}