using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniScraperDLL.Managers
{
    public static class DatabaseManager
    {
        /// <summary>
        /// Connects to an sqlite database file.
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <returns></returns>
        public static SqliteConnection ConnectToDatabase(string databaseFile)
        {
            SqliteConnection connection = new SqliteConnection($"Data Source={databaseFile}");
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Closes a connection to an sqlite database file.
        /// </summary>
        /// <param name="connection"></param>
        public static void CloseConnection(SqliteConnection connection)
        {
            connection.Close();
        }
    }
}