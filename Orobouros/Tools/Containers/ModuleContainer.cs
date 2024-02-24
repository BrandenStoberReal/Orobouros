using Orobouros.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Tools.Containers
{
    /// <summary>
    /// Class designed to hold a list of modules.
    /// </summary>
    public class ModuleContainer
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Module> Modules { get; set; } = new List<Module>();

        public ModuleContainer(string? name = null, string? description = null)
        {
            if (name != null)
            {
                Name = name;
            }
            if (description != null)
            {
                Description = description;
            }
        }
    }
}