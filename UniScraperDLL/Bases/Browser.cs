using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumUndetectedChromeDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UniScraperDLL.Bases
{
    public class Browser
    {
        /// <summary>
        /// Undetected Chrome Driver instance.
        /// </summary>
        public UndetectedChromeDriver WebDriver { get; private set; }

        public WebDriverWait WebDriverWait { get; private set; }

        /// <summary>
        /// Instantiator
        /// </summary>
        /// <param name="profileName">Selenium profile name. Defaults to "uniscraper".</param>
        /// <param name="headless">
        /// Whether the Selenium instance runs headless. Some websites detect this. Defaults to true.
        /// </param>
        public Browser(string dataDir = "./uniscraper_profile", bool headless = true, int timeout = 10)
        {
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--window-size=1920,1080");
            WebDriver = UndetectedChromeDriver.Create(driverExecutablePath: new ChromeDriverInstaller().Auto().Result, options: chromeOptions, headless: headless, userDataDir: Path.GetFullPath(dataDir));
            WebDriverWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeout));
        }

        public void WaitForElement(By elementLocator)
        {
            WebDriverWait.Until(ExpectedConditions.ElementExists(elementLocator));
        }

        /// <summary>
        /// Clean up any background instances.
        /// </summary>
        public void Dispose()
        {
            WebDriver.Quit();
        }
    }
}