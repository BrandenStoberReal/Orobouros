using Orobouros.Bases;
using Orobouros.Managers;
using Orobouros.Tools.Web;
using static Orobouros.UniAssemblyInfo;

namespace OrobourosTests.DLL
{
    [TestClass]
    public class ScraperTests
    {
        public string KemonoTestCreator = "https://kemono.su/fanbox/user/3316400";
        public string CoomerTestCreator = "https://coomer.su/onlyfans/user/belledelphine";

        [TestMethod(displayName: "Scrape Handler - Standard Scrape")]
        public void Test_Basic_Scrape()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Text };
            ModuleData? data = ScrapingManager.ScrapeURL("https://www.test.com/posts/posthere", requestedInfo);
            ScrapingManager.FlushSupplementaryMethods(); // Stop background methods
            LoggingManager.WriteToDebugLog($"Data Length: {data.Content.Count}");
            Assert.IsNotNull(data);
        }

        [TestMethod(displayName: "PartyModule - 1 Kemono Post")]
        public void Test_Kemono_Scrape_1()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
            ModuleData? data = ScrapingManager.ScrapeURL(KemonoTestCreator, requestedInfo, 1);
            ScrapingManager.FlushSupplementaryMethods(); // Stop background methods
            Post fetchedPost = (Post)data.Content.First().Value;
            LoggingManager.WriteToDebugLog($"Post Author: {fetchedPost.Author.Username}");
            LoggingManager.WriteToDebugLog($"Post Title: {fetchedPost.Title}");
            LoggingManager.WriteToDebugLog($"Post Comments: {fetchedPost.Comments.Count}");
            foreach (Attachment attach in fetchedPost.Attachments)
            {
                LoggingManager.WriteToDebugLog($"Post Attachment: {attach.AttachmentType} | {attach.Name}");
            }
            LoggingManager.WriteToDebugLog($"Upload Date: {fetchedPost.UploadDate.ToString()}");
            Assert.IsTrue(data.Content.Count == 1);
        }

        [TestMethod(displayName: "PartyModule - 1 Coomer Post")]
        public void Test_Coomer_Scrape_1()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
            ModuleData? data = ScrapingManager.ScrapeURL(CoomerTestCreator, requestedInfo, 1);
            ScrapingManager.FlushSupplementaryMethods(); // Stop background methods
            Post fetchedPost = (Post)data.Content.First().Value;
            LoggingManager.WriteToDebugLog($"Post Author: {fetchedPost.Author.Username}");
            LoggingManager.WriteToDebugLog($"Post Comments: {fetchedPost.Comments.Count}");
            foreach (Attachment attach in fetchedPost.Attachments)
            {
                LoggingManager.WriteToDebugLog($"Post Attachment: {attach.AttachmentType} | {attach.Name}");
            }
            LoggingManager.WriteToDebugLog($"Upload Date: {fetchedPost.UploadDate.ToString()}");
            Assert.IsTrue(data.Content.Count == 1);
        }

        [TestMethod(displayName: "PartyModule - 10 Kemono Posts")]
        public void Test_Kemono_Scrape_10()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
            ModuleData? data = ScrapingManager.ScrapeURL(KemonoTestCreator, requestedInfo, 10);
            ScrapingManager.FlushSupplementaryMethods(); // Stop background methods
            LoggingManager.WriteToDebugLog($"Kemono Data Length: {data.Content.Count}");
            Assert.IsTrue(data.Content.Count == 10);
        }

        [TestMethod(displayName: "PartyModule - 10 Coomer Posts")]
        public void Test_Coomer_Scrape_10()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
            ModuleData? data = ScrapingManager.ScrapeURL(CoomerTestCreator, requestedInfo, 10);
            ScrapingManager.FlushSupplementaryMethods(); // Stop background methods
            LoggingManager.WriteToDebugLog($"Coomer Data Length: {data.Content.Count}");
            Assert.IsTrue(data.Content.Count == 10);
        }

        [TestMethod(displayName: "PartyModule - 50 Kemono Posts (1 page)")]
        public void Test_Kemono_Scrape_50()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
            ModuleData? data = ScrapingManager.ScrapeURL(KemonoTestCreator, requestedInfo, 50);
            ScrapingManager.FlushSupplementaryMethods(); // Stop background methods
            LoggingManager.WriteToDebugLog($"Kemono Data Length: {data.Content.Count}");
            Assert.IsTrue(data.Content.Count == 50);
        }

        [TestMethod(displayName: "PartyModule - 50 Coomer Posts (1 page)")]
        public void Test_Coomer_Scrape_50()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
            ModuleData? data = ScrapingManager.ScrapeURL(CoomerTestCreator, requestedInfo, 50);
            ScrapingManager.FlushSupplementaryMethods(); // Stop background methods
            LoggingManager.WriteToDebugLog($"Coomer Data Length: {data.Content.Count}");
            Assert.IsTrue(data.Content.Count == 50);
        }

        [TestMethod(displayName: "PartyModule - 70 Kemono Posts (1.5 pages)")]
        public void Test_Kemono_Scrape_70()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
            ModuleData? data = ScrapingManager.ScrapeURL(KemonoTestCreator, requestedInfo, 70);
            ScrapingManager.FlushSupplementaryMethods(); // Stop background methods
            LoggingManager.WriteToDebugLog($"Kemono Data Length: {data.Content.Count}");
            Assert.IsTrue(data.Content.Count == 70);
        }

        [TestMethod(displayName: "PartyModule - 70 Coomer Posts (1.5 pages)")]
        public void Test_Coomer_Scrape_70()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
            ModuleData? data = ScrapingManager.ScrapeURL(CoomerTestCreator, requestedInfo, 70);
            ScrapingManager.FlushSupplementaryMethods(); // Stop background methods
            LoggingManager.WriteToDebugLog($"Coomer Data Length: {data.Content.Count}");
            Assert.IsTrue(data.Content.Count == 70);
        }
    }
}