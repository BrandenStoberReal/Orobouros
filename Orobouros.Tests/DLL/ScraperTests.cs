using Orobouros.Bases;
using Orobouros.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Orobouros.UniAssemblyInfo;

namespace OrobourosTests.DLL
{
    [TestClass]
    public class ScraperTests
    {
        [TestMethod(displayName: "Scrape Handler - Standard Scrape")]
        public void Test_Basic_Scrape()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Text };
            ModuleData? data = ScrapingManager.ScrapeURL("https://www.test.com/posts/posthere", requestedInfo);
            ScrapingManager.FlushSupplementaryMethods(); // Stop background methods
            DebugManager.WriteToDebugLog($"Data Length: {data.Content.Count}");
            Assert.IsNotNull(data);
        }

        [TestMethod(displayName: "Module Scrape Handler - 10 Kemono Posts")]
        public void Test_Kemono_Scrape_10()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
            ModuleData? data = ScrapingManager.ScrapeURL("https://kemono.su/fantia/user/3959", requestedInfo, 10);
            ScrapingManager.FlushSupplementaryMethods(); // Stop background methods
            DebugManager.WriteToDebugLog($"Kemono Data Length: {data.Content.Count}");
            Assert.IsTrue(data.Content.Count == 10);
        }

        [TestMethod(displayName: "Module Scrape Handler - 10 Coomer Posts")]
        public void Test_Coomer_Scrape_10()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
            ModuleData? data = ScrapingManager.ScrapeURL("https://coomer.su/onlyfans/user/belledelphine", requestedInfo, 10);
            ScrapingManager.FlushSupplementaryMethods(); // Stop background methods
            DebugManager.WriteToDebugLog($"Coomer Data Length: {data.Content.Count}");
            Assert.IsTrue(data.Content.Count == 10);
        }
    }
}