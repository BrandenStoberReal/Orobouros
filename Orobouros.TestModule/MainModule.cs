using FlyingSubmarineDLL.Attributes;
using System.Runtime.InteropServices;
using Orobouros.Bases;
using static Orobouros.UniAssemblyInfo;
using Orobouros.Attributes;

namespace Orobouros.TestModule
{
    // Module must inherit ScraperInfo.
    [OrobourosModule("Test Module", "A simple test module", "v1.0.0.0")]
    public class MainModule : ModuleInfo
    {
        // Module information.

        [ModuleSites]
        public override List<string> SupportedWebsites { get; set; } = new List<string>
        {
            "https://www.test.com",
            "https://www.test2.com"
        };

        [ModuleContents]
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

        // Supplementary method. This is run every framework execution cycle in a separate thread
        // collectively with all other loaded module's background work. If you want concurrency,
        // declare a new thread here yourself.
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