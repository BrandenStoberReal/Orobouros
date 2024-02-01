using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniScraperDLL.Bases;

namespace UniScraperDLLTests.DLL
{
    [TestClass]
    public class BrowserTests
    {
        [TestMethod(displayName: "Browser Test - Fetch")]
        public async Task Browser_Fetch_Test()
        {
            var browser = new Browser();
            browser.WebDriver.GoTo("https://www.google.com/");
            Assert.IsTrue(browser.WebDriver.PageSource != "");
            browser.Dispose();
        }
    }
}