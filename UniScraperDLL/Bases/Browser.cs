using Selenium.Extensions;
using Selenium.WebDriver.UndetectedChromeDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniScraperDLL.Bases
{
    public class Browser
    {
        /// <summary>
        /// Undetected Chrome Driver instance.
        /// </summary>
        public SlDriver WebDriver { get; private set; }

        /// <summary>
        /// Instantiator
        /// </summary>
        /// <param name="profileName">Selenium profile name. Defaults to "uniscraper".</param>
        /// <param name="headless">
        /// Whether the Selenium instance runs headless. Some websites detect this. Defaults to true.
        /// </param>
        public Browser(string profileName = "uniscraper", bool headless = true)
        {
            WebDriver = UndetectedChromeDriver.Instance(profileName, Headless: headless);
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