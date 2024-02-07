using FlyingSubmarineDLL.Attributes;
using UniScraperDLL.Bases;
using static UniScraperDLL.UniAssemblyInfo;

namespace UniScraper.TestModule
{
    // Module must inherit ScraperInfo.
    [FlyingSubmarineModule("Test Module", "A simple test module", "v1.0.0.0")]
    public class MainModule : ModuleInfo
    {
        // Module information.

        public override List<string> SupportedWebsites { get; set; } = new List<string>
        {
            "https://www.test.com",
            "https://www.test2.com"
        };

        public override List<ModuleContent> SupportedContent { get; set; } = new List<ModuleContent>
        {
            ModuleContent.Text
        };

        // Module methods.

        // Initializer method. Used to run code when the module loads. Always run on a new thread.
        [ModuleInit]
        public void Initialize()
        {
            System.Diagnostics.Trace.WriteLine($"Hello from TestModule init!");
        }

        // Scrape method. Called whenever the framework recieves a scrape request and this module
        // matches the requested content.
        [ModuleScrape]
        public ModuleData Scrape(string scrapeUrl, int requestedObjs)
        {
            ModuleData data = new ModuleData();
            data.ContentType = ModuleContent.Text;
            data.Content.Add("Hello World!");
            data.RequestedDataAmount = requestedObjs;
            data.Website = scrapeUrl;
            return data;
        }

        // Supplementary method. This is run every framework execution cycle in a separate thread.
        [ModuleSupplementary]
        public void DoBackgroundWork()
        {
            if (0 == 1)
            {
                System.Diagnostics.Trace.WriteLine($"Quantom bitflip!");
            }
        }
    }
}