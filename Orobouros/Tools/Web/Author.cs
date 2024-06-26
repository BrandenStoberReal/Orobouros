﻿using ProtoBuf;

namespace Orobouros.Tools.Web
{
    /// <summary>
    /// An individual or group. This class is designed for anything that digitally represents a user online.
    /// </summary>
    [ProtoContract]
    public class Author
    {
        /// <summary>
        /// User's username.
        /// </summary>
        [ProtoMember(1)]
        public string Username { get; set; }

        /// <summary>
        /// URL to user's profile page, if applicable.
        /// </summary>
        [ProtoMember(2)]
        public string URL { get; set; }

        /// <summary>
        /// User's first name, if applicable.
        /// </summary>
        [ProtoMember(3)]
        public string? FirstName { get; set; }

        /// <summary>
        /// User's last name, if applicable.
        /// </summary>
        [ProtoMember(4)]
        public string? LastName { get; set; }

        /// <summary>
        /// User's region, if known or applicable.
        /// </summary>
        [ProtoMember(5)]
        public string? Region { get; set; }

        /// <summary>
        /// User's primary language, if known or applicable.
        /// </summary>
        [ProtoMember(6)]
        public string? Language { get; set; }

        /// <summary>
        /// A link to the user's profile picture.
        /// </summary>
        [ProtoMember(7)]
        public string? ProfilePicture { get; set; }

        /// <summary>
        /// Date on which the user's account was created.
        /// </summary>
        [ProtoMember(8)]
        public DateTime? AccountCreationDate { get; set; }

        /// <summary>
        /// Date on which the user last logged in, if applicable.
        /// </summary>
        [ProtoMember(9)]
        public DateTime? LastOnline { get; set; }
    }
}