using Orobouros.Tools.Web;

namespace Orobouros.Tools.Containers
{
    /// <summary>
    /// Class designed to hold posts.
    /// </summary>
    public class PostContainer
    {
        /// <summary>
        /// Descriptive name for this container.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description for this container.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Exact date and time this container was instantiated.
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Container's child posts.
        /// </summary>
        public List<Post> Posts { get; set; } = new List<Post>();

        public PostContainer(string? name = null, string? description = null)
        {
            if (name != null)
            {
                Name = name;
            }
            if (description != null)
            {
                Description = description;
            }
            CreationTime = DateTime.Now;
        }
    }
}