using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Bases
{
    public class SqliteColumn
    {
        public string Name { get; set; }
        public string ValueType { get; set; }

        public SqliteColumn(string name, string valuetype)
        {
            Name = name;
            ValueType = valuetype;
        }
    }
}