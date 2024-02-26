using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Bases
{
    /// <summary>
    /// Represents a column in SQLite.
    /// </summary>
    public class SqliteColumn
    {
        /// <summary>
        /// Column name. Should probably be unique.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of SQLite data this column holds.
        /// </summary>
        public string ValueType { get; set; }

        public SqliteColumn(string name, string valuetype)
        {
            Name = name;
            ValueType = valuetype;
        }
    }
}