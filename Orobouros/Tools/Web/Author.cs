using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Tools.Web
{
    /// <summary>
    /// An individual or group. This class is designed for anything that digitally represents a user online.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// User's username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// URL to user's profile page, if applicable.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// User's first name, if applicable.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// User's last name, if applicable.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// User's region, if known or applicable.
        /// </summary>
        public string? Region { get; set; }

        /// <summary>
        /// User's primary language, if known or applicable.
        /// </summary>
        public string? Language { get; set; }

        /// <summary>
        /// A link to the user's profile picture.
        /// </summary>
        public string? ProfilePicture { get; set; }

        /// <summary>
        /// Date on which the user's account was created.
        /// </summary>
        public DateTime? AccountCreationDate { get; set; }

        /// <summary>
        /// Date on which the user last logged in, if applicable.
        /// </summary>
        public DateTime? LastOnline { get; set; }
    }
}