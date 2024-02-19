using Microsoft.Data.Sqlite;
using Orobouros.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}