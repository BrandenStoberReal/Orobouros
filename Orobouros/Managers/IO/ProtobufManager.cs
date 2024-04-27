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
        /// <param name="newData"></param>
        public static void AddPostsToCache(Module mod, List<ProcessedScrapeData> newData)
        {
            // Add new posts to cache
            if (newData.Any(x => x.ContentType == ContentType.Subposts))
            {
                // For each new bundle of data
                foreach (ProcessedScrapeData data in newData.Where(x => x.ContentType == ContentType.Subposts))
                {
                    Post post = (Post)data.Value;

                    // See if cached post exists before remaking it
                    if (IsPostInCache(mod, post.URL))
                    {
                        continue;
                    }

                    // Create cache file
                    using (var file = File.Create(mod.ProtobufDirectory + $"/posts/{post.CacheID}.bin"))
                    {
                        Serializer.Serialize(file, post);

                        // Purge old cached post if it was stale
                        if (mod.CachedPosts.Any(x => x.URL == post.URL))
                        {
                            mod.CachedPosts.Remove(mod.CachedPosts.First(x => x.URL == post.URL));
                        }
                        mod.CachedPosts.Add(post);
                    }
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
            if (mod.CachedPosts.Any(x => x.URL == postURL))
            {
                // Post exists in cache
                if (respectStale)
                {
                    if ((DateTime.Now - mod.CachedPosts.First(x => x.URL == postURL).ScrapeDate).Days > CacheCycleInterval)
                    {
                        // Post exists in cache, but is stale
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
    }
}