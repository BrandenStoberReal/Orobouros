using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniScraperDLL.Bases;
using UniScraperDLL.Managers;

namespace UniScraperDLLTests.DLL
{
    [TestClass]
    public class BrowserTests
    {
        [TestMethod(displayName: "Browser Test - Fetch")]
        public async Task Browser_Fetch_Test()
        {
            var browser = new Browser();
            browser.WebDriver.GoToUrl("https://www.google.com/");
            Assert.IsTrue(browser.WebDriver.PageSource != "");
            browser.Dispose();
        }

        [TestMethod(displayName: "Browser Test - Antibot Test")]
        public async Task Browser_Antibot_Test()
        {
            var browser = new Browser(headless: false);
            try
            {
                browser.WebDriver.GoToUrl("https://www.nowsecure.nl/#relax");
                browser.WaitForElement(By.ClassName("nonhystericalbg"));
                browser.Dispose();
            }
            catch (Exception ex)
            {
                browser.Dispose();
                Assert.Fail();
            }
        }
    }
}