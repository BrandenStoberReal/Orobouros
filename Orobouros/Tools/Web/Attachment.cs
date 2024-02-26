using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Orobouros.UniAssemblyInfo;

namespace Orobouros.Tools.Web
{
    /// <summary>
    /// An attachment on a post. This can be any number of things, most commonly images and files.
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Attachment file/container/whatever name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL to the attachment resource.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Attachment's parent post.
        /// </summary>
        public Post ParentPost { get; set; }

        /// <summary>
        /// Type of content this attachment encapsulates.
        /// </summary>
        public AttachmentContent AttachmentType { get; set; }

        /// <summary>
        /// Raw attachment data.
        /// </summary>
        public Stream? Binary { get; set; }
    }
}