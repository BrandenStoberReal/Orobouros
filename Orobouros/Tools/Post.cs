using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Tools
{
    /// <summary>
    /// Generic post class that modules can freely use. This allows modules to return subposts
    /// instead of creating their own class which cannot be parsed by the main library.
    /// </summary>
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string URL { get; set; }
    }
}