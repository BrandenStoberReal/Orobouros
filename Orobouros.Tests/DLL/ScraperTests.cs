using Orobouros.Bases;
using Orobouros.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Orobouros.UniAssemblyInfo;

namespace Orobouros.Tests.DLL
{
    [TestClass]
    public class ScraperTests
    {
        [TestMethod(displayName: "Scrape Handler - Standard Scrape")]
        public void Test_Basic_Scrape()
        {
            ScrapingManager.InitializeModules();
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Text };
            ModuleData? data = ScrapingManager.ScrapeURL("test.com", "https://www.test.com/posts/posthere", requestedInfo, 1);
            ScrapingManager.SupplementaryCancelToken.Cancel(); // Stop background methods
            DebugManager.WriteToDebugLog($"Data Length: {data.Content.Count}");
            Assert.IsNotNull(data);
        }
    }
}