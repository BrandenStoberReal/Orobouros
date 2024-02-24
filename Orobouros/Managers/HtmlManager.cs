using HtmlAgilityPack;
using Orobouros.Tools.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Managers
{
    public static class HtmlManager
    {
        /// <summary>
        /// Selects nodes by class name and optionally node tag type.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="className"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static List<HtmlNode> SelectNodesByClass(HtmlDocument document, string className, string nodeName = "")
        {
            if (nodeName != "")
            {
                return document.DocumentNode.Descendants().Where(x => x.HasClass(className) && x.Name == nodeName).ToList();
            }
            else
            {
                return document.DocumentNode.Descendants().Where(x => x.HasClass(className)).ToList();
            }
        }

        /// <summary>
        /// Selects nodes of a specific tag type. Example: "img"
        /// </summary>
        /// <param name="document"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static List<HtmlNode> SelectNodesByType(HtmlDocument document, string nodeName)
        {
            return document.DocumentNode.Descendants().Where(x => x.Name == nodeName).ToList();
        }

        /// <summary>
        /// Selects nodes by attribute, and optionally a keyword to search for in the attribute's value.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValueKeyword"></param>
        /// <returns></returns>
        public static List<HtmlNode> SelectNodesByAttribute(HtmlDocument document, string attributeName, string attributeValueKeyword = "")
        {
            if (attributeValueKeyword != "")
            {
                return document.DocumentNode.Descendants().Where(x => x.Attributes[attributeName] != null && x.Attributes[attributeName].Value.Contains(attributeValueKeyword)).ToList();
            }
            else
            {
                return document.DocumentNode.Descendants().Where(x => x.Attributes[attributeName] != null).ToList();
            }
        }

        /// <summary>
        /// Selects all image nodes with URLs that contain the specified keyword.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static List<HtmlNode> SelectImageNodesByKeyword(HtmlDocument document, string keyword)
        {
            return document.DocumentNode.Descendants().Where(x => x.Name == "img" && x.Attributes["src"] != null && x.Attributes["src"].Value.Contains(keyword)).ToList();
        }

        /// <summary>
        /// Selects all nodes with links that contain the specified keyword in the URL.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static List<HtmlNode> SelectLinkNodesByKeyword(HtmlDocument document, string keyword)
        {
            return SelectNodesByAttribute(document, "href", keyword);
        }

        /// <summary>
        /// Fetches all images in the HTML document.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static List<HtmlNode> FetchImageNodes(HtmlDocument document)
        {
            return SelectNodesByType(document, "img");
        }

        /// <summary>
        /// Fetches all nodes in the HTML document with a link attached to them.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static List<HtmlNode> FetchLinkNodes(HtmlDocument document)
        {
            return SelectNodesByAttribute(document, "href");
        }

        /// <summary>
        /// Fetches all buttons in the HTML document.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static List<HtmlNode> FetchButtonNodes(HtmlDocument document)
        {
            return SelectNodesByType(document, "button");
        }

        /// <summary>
        /// Fetches all JS nodes in the HTML document.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static List<HtmlNode> FetchJavascriptNodes(HtmlDocument document)
        {
            return SelectNodesByType(document, "script");
        }

        public static HtmlNode? FetchNodeByXPath(HtmlDocument document, string xpath)
        {
            return document.DocumentNode.SelectSingleNode(xpath);
        }

        public static List<HtmlNode> FetchChildNodes(HtmlNode parent)
        {
            List<HtmlNode> nodes = new List<HtmlNode>();
            foreach (var post in parent.ChildNodes)
            {
                if (post.NodeType == HtmlNodeType.Element)
                {
                    nodes.Add(post);
                }
            }
            return nodes;
        }
    }
}