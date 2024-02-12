using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Managers
{
    public static class DebugManager
    {
        public static void WriteToDebugLog(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
        }
    }
}