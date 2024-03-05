using Microsoft.Data.Sqlite;
using Orobouros.Bases;
using System.Data;

namespace Orobouros.Managers
{
    /// <summary>
    /// Handles all of the framework's database operations.
    /// </summary>
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
        /// Creates an empty database file.
        /// </summary>
        /// <param name="parentFolder"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static string CreateDatabase(string parentFolder, string databaseName)
        {
            string fullPath = Path.Combine(parentFolder, $"{databaseName}.sqlite");
            if (!File.Exists(fullPath))
            {
                File.WriteAllBytes(fullPath, new byte[0]);
            }
            return fullPath;
        }

        /// <summary>
        /// Creates a table with an optional list of columns.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        public static void CreateTable(SqliteConnection connection, string tableName, List<SqliteColumn> columns = null)
        {
            List<string> keyValuePairs = new List<string>();
            if (columns == null)
            {
                ExecuteQuery(connection, $"CREATE TABLE IF NOT EXISTS {tableName} ();");
            }
            else
            {
                foreach (SqliteColumn column in columns)
                {
                    keyValuePairs.Add($"{column.Name} {column.ValueType}");
                }
                ExecuteQuery(connection, $"CREATE TABLE IF NOT EXISTS {tableName} ({String.Join(",\n", keyValuePairs)});");
            }
        }

        /// <summary>
        /// Drops a table registered in the database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        public static void DeleteTable(SqliteConnection connection, string tableName)
        {
            ExecuteQuery(connection, $"DROP TABLE IF EXISTS {tableName};");
        }

        /// <summary>
        /// Executes a SQL query against an SQLite database. Returns the result set for SELECT
        /// queries, or null for INSERT, UPDATE, DELETE queries if they execute successfully.
        /// </summary>
        /// <param name="connection">An SqliteConnection object reference..</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="parameters">Optional parameters for the SQL query.</param>
        /// <returns>
        /// A DataTable containing the result set for SELECT queries, or null for other query types.
        /// </returns>
        public static (DataTable? dataTable, long? affectedRowsOrInsertId) ExecuteQuery(SqliteConnection connection, string sqlQuery, Dictionary<string, object> parameters = null)
        {
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