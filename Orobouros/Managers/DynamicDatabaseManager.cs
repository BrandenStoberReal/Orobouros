using Orobouros.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Managers
{
    public class DynamicDatabaseManager
    {
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

        public static string FetchModuleDatabasePath(Module mod)
        {
            VerifyStorageLocation(mod);
            return $"./moduledb/{mod.GUID}/{mod.GUID}.sqlite";
        }
    }
}