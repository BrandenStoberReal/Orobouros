using Microsoft.Data.Sqlite;
using Orobouros.Bases;
using Orobouros.Managers;
using Orobouros.Managers.Database;
using Orobouros.Managers.Modules;

namespace OrobourosTests.DLL
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod(displayName: "Database Tests - Create Database")]
        public void Test_Database_Creation()
        {
            string dbName = "test";
            string parentFolder = "./";
            string fullPath = Path.Combine(parentFolder, $"{dbName}.sqlite");
            DatabaseManager.CreateDatabase(parentFolder, dbName);
            if (!File.Exists(fullPath))
            {
                Assert.Fail();
            }
        }

        [TestMethod(displayName: "Database Tests - Create Database Connection")]
        public void Test_Database_CreateCon()
        {
            string dbName = "test";
            string parentFolder = "./";
            string fullPath = Path.Combine(parentFolder, $"{dbName}.sqlite");
            DatabaseManager.CreateDatabase(parentFolder, dbName);
            if (!File.Exists(fullPath))
            {
                Assert.Fail();
            }
            else
            {
                SqliteConnection con = DatabaseManager.ConnectToDatabase(fullPath);
                Assert.IsNotNull(con);
            }
        }

        [TestMethod(displayName: "Database Tests - Test Dynamic Database Directory")]
        public void Test_Database_DynDir()
        {
            ModuleManager.LoadAssemblies();
            foreach (Module module in ModuleManager.Container.Modules)
            {
                string path = DynamicDatabaseManager.FetchModuleDatabasePath(module);
                Assert.IsTrue(Path.Exists(path));
            }
        }
    }
}