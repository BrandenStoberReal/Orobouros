using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Tools.Web
{
    /// <summary>
    /// Generic post class that modules can freely use. This allows modules to return subposts
    /// instead of creating their own class which cannot be parsed by the main library.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Unique ID of the post. Every site typically handles this differently.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Post title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Post description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Post author.
        /// </summary>
        public Author? Author { get; set; }

        /// <summary>
        /// Post URL.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Date the post was first uploaded.
        /// </summary>
        public DateTime? UploadDate { get; set; }

        /// <summary>
        /// Date the post was most recently edited.
        /// </summary>
        public DateTime? LastEditedDate { get; set; }

        /// <summary>
        /// Any attachments associated with the post.
        /// </summary>
        public List<Attachment>? Attachments { get; set; }

        /// <summary>
        /// Any replies/comments to the post.
        /// </summary>
        public List<Comment>? Comments { get; set; }
    }
}