using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Tools.Web
{
    public class Comment
    {
        public Post ParentPost { get; set; }
        public string URL { get; set; }
        public Author Author { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime LastEdited { get; set; }
        public string Content { get; set; }
        public List<Comment> Replies { get; set; } = new List<Comment>();
    }
}