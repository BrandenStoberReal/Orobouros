using Orobouros.Managers.Misc;
using ProtoBuf;

namespace Orobouros.Tools.Web
{
    /// <summary>
    /// Generic post class that modules can freely use. This allows modules to return subposts
    /// instead of creating their own class which cannot be parsed by the main library.
    /// </summary>
    [ProtoContract]
    public class Post
    {
        /// <summary>
        /// Unique ID of the post. Every site typically handles this differently.
        /// </summary>
        [ProtoMember(1)]
        public string? Id { get; set; }

        /// <summary>
        /// Post title.
        /// </summary>
        [ProtoMember(2)]
        public string Title { get; set; }

        /// <summary>
        /// Post description.
        /// </summary>
        [ProtoMember(3)]
        public string? Description { get; set; }

        /// <summary>
        /// Post author.
        /// </summary>
        [ProtoMember(4)]
        public Author? Author { get; set; }

        /// <summary>
        /// Post URL.
        /// </summary>
        [ProtoMember(5)]
        public string URL { get; set; }

        /// <summary>
        /// Date the post was first uploaded.
        /// </summary>
        [ProtoMember(6)]
        public DateTime? UploadDate { get; set; }

        /// <summary>
        /// Date the post was most recently edited.
        /// </summary>
        [ProtoMember(7)]
        public DateTime? LastEditedDate { get; set; }

        /// <summary>
        /// Any attachments associated with the post.
        /// </summary>
        [ProtoMember(8)]
        public List<Attachment>? Attachments { get; set; } = new List<Attachment>();

        /// <summary>
        /// Any replies/comments to the post.
        /// </summary>
        [ProtoMember(9)]
        public List<Comment>? Comments { get; set; } = new List<Comment>();

        /// <summary>
        /// Internal ID for protobuf caching.
        /// </summary>
        [ProtoMember(10)]
        public string CacheID { get; set; } = StringManager.RandomString(32);

        /// <summary>
        /// Time this post was scraped. Used for cache cycling.
        /// </summary>
        [ProtoMember(11)]
        public DateTime ScrapeDate { get; set; } = DateTime.Now;
    }
}