﻿using Orobouros.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Managers
{
    public class DynamicDatabaseManager
    {
        /// <summary>
        /// Verifies a module's storage directory exists.
        /// </summary>
        /// <param name="mod"></param>
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
        /// <param name="mod"></param>
        /// <returns></returns>
        public static string FetchModuleDatabasePath(Module mod)
        {
            VerifyStorageLocation(mod);
            return Path.GetFullPath($"./moduledb/{mod.GUID}/{mod.GUID}.sqlite");
        }
    }
}