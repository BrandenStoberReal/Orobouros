using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orobouros.Tools.Web;

namespace Orobouros.Tools.Containers
{
    /// <summary>
    /// Class designed to hold posts.
    /// </summary>
    public class PostContainer
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}