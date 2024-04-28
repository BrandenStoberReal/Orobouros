using Microsoft.EntityFrameworkCore.Infrastructure;
using Orobouros.Bases;
using Orobouros.Tools.Web;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Orobouros.Orobouros;

namespace Orobouros.Managers.IO
{
    public static class ProtobufManager
    {
        /// <summary>
        /// The time period, in days, to remove stale cache instances.
        /// </summary>
        public static int CacheCycleInterval = 7;

        /// <summary>
        /// Adds newly-scraped post instances to a module's caching directory.
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="newData">Returned scraper output data</param>
        public static void AddPostDataToCache(Module mod, List<ProcessedScrapeData> newData)
        {
            // Enumerate through posts embedded in the scraper output
            if (newData.Any(x => x.ContentType == ContentType.Subposts))
            {
                // For each new bundle of post data
                foreach (ProcessedScrapeData data in newData.Where(x => x.ContentType == ContentType.Subposts))
                {
                    // Instantiate the bundled data value
                    Post post = (Post)data.Value;

                    CachePost(mod, post);
                }
            }
        }

        /// <summary>
        /// Determines whether a post is in the protobuf cache or not
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="postURL"></param>
        /// <param name="respectStale"></param>
        /// <returns></returns>
        public static bool IsPostInCache(Module mod, string postURL, bool respectStale = true)
        {
            // Enumerate through cached posts
            if (mod.CachedPosts.Any(x => x.URL == postURL))
            {
                // Post exists in cache
                if (respectStale)
                {
                    if ((DateTime.Now - mod.CachedPosts.First(x => x.URL == postURL).ScrapeDate).Days > CacheCycleInterval)
                    {
                        // Post exists in cache, but is stale
                        mod.CachedPosts.Remove(mod.CachedPosts.First(x => x.URL == postURL)); // Remove stale cache from memory
                        return false;
                    }
                    else
                    {
                        // Post exists in cache, and is not stale
                        return true;
                    }
                }
                else
                {
                    // Ignore staleness and return true regardless
                    return true;
                }
            }
            else
            {
                // Post does not exist in cache
                return false;
            }
        }

        /// <summary>
        /// Obtains a cached post instance.
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="postURL"></param>
        /// <returns>The cached post. Returns null if the post is not cached/not found.</returns>
        public static Post? FetchCachedPost(Module mod, string postURL)
        {
            if (mod.CachedPosts.Any(x => x.URL == postURL))
            {
                return mod.CachedPosts.First(x => x.URL == postURL);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a single post to the module cache. Used internally for caching operations, but can be used publically aswell.
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="post"></param>
        public static void CachePost(Module mod, Post post)
        {
            // See if cached post exists already
            if (IsPostInCache(mod, post.URL))
            {
                return;
            }

            // Create cache file
            using (var file = File.Create(mod.ProtobufDirectory + $"/posts/{post.CacheID}.bin"))
            {
                Serializer.Serialize(file, post);
                mod.CachedPosts.Add(post);
            }
        }
    }
}