using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UniScraperDLL.Bases;

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

        /// <summary>
        /// Executes a SQL query against an SQLite database. Returns the result set for SELECT
        /// queries, or null for INSERT, UPDATE, DELETE queries if they execute successfully.
        /// </summary>
        /// <param name="databaseFile">The path to the SQLite database file.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="parameters">Optional parameters for the SQL query.</param>
        /// <returns>
        /// A DataTable containing the result set for SELECT queries, or null for other query types.
        /// </returns>
        public static (DataTable? dataTable, long? affectedRowsOrInsertId) ExecuteQuery(string databaseFile, string sqlQuery, Dictionary<string, object> parameters = null)
        {
            var connectionString = $"Data Source={databaseFile}";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sqlQuery, connection);

            // Add parameters to the command if any
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }

            // Determine the type of query
            if (sqlQuery.Trim().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            {
                // It's a SELECT query, so execute and return results
                using var adapter = command.ExecuteReader();
                DataTable? dataTable = new DataTable();
                dataTable.Load(adapter);
                return (dataTable, null);
            }
            else
            {
                // For INSERT, UPDATE, DELETE, execute the command without returning data
                long? affectedRows = command.ExecuteNonQuery();

            long? insertId = null;

            if (sqlQuery.Trim().StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
            {
                command.CommandText = "SELECT last_insert_rowid()";
                insertId = (long?)command.ExecuteScalar();
            }
                return (null, insertId ?? affectedRows);
            }
        }
    }
}