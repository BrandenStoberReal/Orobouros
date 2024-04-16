using Orobouros.Bases;
using Orobouros.Managers.Database;

namespace Orobouros.Managers.Database
{
    /// <summary>
    /// Manages an abstraction layer between the modules and the database manager.
    /// </summary>
    public static class DynamicDatabaseManager
    {
        /// <summary>
        /// Verifies a module's storage directory exists.
        /// </summary>
        /// <param name="mod">Specified module to check</param>
        public static void VerifyStorageLocation(Module mod)
        {
            if (!Directory.Exists("./moduledb"))
            {
                Directory.CreateDirectory("./moduledb");
            }

            if (!Directory.Exists("./moduledb/" + mod.GUID))
            {
                Directory.CreateDirectory($"./moduledb/{mod.GUID}");
            }

            if (!File.Exists($"./moduledb/{mod.GUID}/{mod.GUID}.sqlite"))
            {
                DatabaseManager.CreateDatabase($"./moduledb/{mod.GUID}/", mod.GUID);
            }
        }

        /// <summary>
        /// Fetches the absolute path for a module's database file.
        /// </summary>
        /// <param name="mod">Specified module to fetch</param>
        /// <returns></returns>
        public static string FetchModuleDatabasePath(Module mod)
        {
            VerifyStorageLocation(mod);
            return Path.GetFullPath($"./moduledb/{mod.GUID}/{mod.GUID}.sqlite");
        }
    }
}