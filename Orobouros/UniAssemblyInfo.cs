using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros
{
    public static class UniAssemblyInfo
    {
        public static string Version = "1.0.0";

        public enum ModuleContent
        {
            Text,
            Files,
            Images,
            Videos,
            Comments,
            Links,
            Other
        }
    }
}