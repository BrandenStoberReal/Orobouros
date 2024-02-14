using Orobouros.Bases;
using Orobouros.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Tests.DLL
{
    [TestClass]
    public class ScraperTests
    {
        [TestMethod(displayName: "Scrape Handler - Standard Scrape")]
        public void Test_Basic_Scrape()
        {
            ScrapingManager.InitializeModules();
            ModuleData? data = ScrapingManager.ScrapeURL("test.com", "https://www.test.com/posts/posthere", new List<UniAssemblyInfo.ModuleContent> { UniAssemblyInfo.ModuleContent.Text }, 1);
            ScrapingManager.SupplementaryCancelToken.Cancel(); // Stop background methods
            Assert.IsNotNull(data);
        }
    }
}