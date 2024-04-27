using ProtoBuf;

namespace Orobouros.Tools.Web
{
    /// <summary>
    /// A comment on a post. Currently only supports text and not embedded images/gifs/videos.
    /// </summary>
    [ProtoContract]
    public class Comment
    {
        /// <summary>
        /// Comment's parent post.
        /// </summary>
        [ProtoMember(1)]
        public Post ParentPost { get; set; }

        /// <summary>
        /// URL of the comment, if applicable.
        /// </summary>
        [ProtoMember(2)]
        public string URL { get; set; }

        /// <summary>
        /// Comment's author.
        /// </summary>
        [ProtoMember(3)]
        public Author Author { get; set; }

        /// <summary>
        /// Post's upload time, if applicable.
        /// </summary>
        [ProtoMember(4)]
        public DateTime PostTime { get; set; }

        /// <summary>
        /// Date on which the post was last edited, if applicable.
        /// </summary>
        [ProtoMember(5)]
        public DateTime LastEdited { get; set; }

        /// <summary>
        /// Comment's text body.
        /// </summary>
        [ProtoMember(6)]
        public string Content { get; set; }

        /// <summary>
        /// Any replies to this comment, if applicable.
        /// </summary>
        [ProtoMember(7)]
        public List<Comment> Replies { get; set; } = new List<Comment>();
    }
}