using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Tools.Web
{
    public class Author
    {
        public string Username { get; set; }
        public string URL { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Region { get; set; }
        public string? Language { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime? AccountCreationDate { get; set; }
        public DateTime? LastOnline { get; set; }
    }
}