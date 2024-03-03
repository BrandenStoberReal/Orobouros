namespace Orobouros.Tools.Web
{
    /// <summary>
    /// A comment on a post. Currently only supports text and not embedded images/gifs/videos.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Comment's parent post.
        /// </summary>
        public Post ParentPost { get; set; }

        /// <summary>
        /// URL of the comment, if applicable.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Comment's author.
        /// </summary>
        public Author Author { get; set; }

        /// <summary>
        /// Post's upload time, if applicable.
        /// </summary>
        public DateTime PostTime { get; set; }

        /// <summary>
        /// Date on which the post was last edited, if applicable.
        /// </summary>
        public DateTime LastEdited { get; set; }

        /// <summary>
        /// Comment's text body.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Any replies to this comment, if applicable.
        /// </summary>
        public List<Comment> Replies { get; set; } = new List<Comment>();
    }
}