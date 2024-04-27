using static Orobouros.Orobouros;
using ProtoBuf;

namespace Orobouros.Tools.Web
{
    /// <summary>
    /// An attachment on a post. This can be any number of things, most commonly images and files.
    /// </summary>
    [ProtoContract]
    public class Attachment
    {
        /// <summary>
        /// Attachment file/container/whatever name.
        /// </summary>
        [ProtoMember(1)]
        public string Name { get; set; }

        /// <summary>
        /// URL to the attachment resource.
        /// </summary>
        [ProtoMember(2)]
        public string URL { get; set; }

        /// <summary>
        /// Attachment's parent post.
        /// </summary>
        [ProtoMember(3)]
        public Post ParentPost { get; set; }

        /// <summary>
        /// Type of content this attachment encapsulates.
        /// </summary>
        [ProtoMember(4)]
        public AttachmentType AttachmentType { get; set; }

        /// <summary>
        /// Raw attachment data. Memory-leaking mess and thus is deprecated.
        /// </summary>
        [Obsolete]
        [ProtoMember(5)]
        public Stream? Binary { get; set; }
    }
}