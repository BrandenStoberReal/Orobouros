using Orobouros.Bases;

namespace Orobouros.Tools.Containers
{
    /// <summary>
    /// Class designed to hold a list of modules.
    /// </summary>
    public class ModuleContainer
    {
        /// <summary>
        /// Descriptive name for the container.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description for the container.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Container's child modules.
        /// </summary>
        public List<Module> Modules { get; set; } = new List<Module>();

        /// <summary>
        /// Container constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
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