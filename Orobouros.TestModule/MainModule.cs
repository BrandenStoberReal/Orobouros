using Orobouros.Attributes;
using Orobouros.Bases;
using static Orobouros.Orobouros;

namespace Orobouros.TestModule
{
    [OrobourosModule("Test Module", "8ff30f03-3446-493c-800f-67b47c83e216", "1.0.0.0", "A simple test module.")]
    public class MainModule
    {
        // <-- Module information -->
        // You can either override these or create your own, although override
        // is strongly encouraged.

        // Websites supported by this module. You can either use loose domains ("google.com") or
        // full URLs ("https://google.com/search?q=test") depending on your code. Website URL to be
        // scraped will be checked if the URL includes this URL at all.
        [ModuleSites]
        public List<string> SupportedWebsites { get; set; } = new List<string>
        {
            "https://www.test.com",
            "https://www.test2.com"
        };

        // Returned content supported by this module. Anything not explicitly listed should be
        // classified as "Other" enum type.
        [ModuleContents]
        public List<ContentType> SupportedContent { get; set; } = new List<ContentType>
        {
            ContentType.Text
        };

        // <-- Module methods -->

        // Initializer method. Used to run code when the module loads. Always run on a new thread.
        [ModuleInit]
        public void Initialize()
        {
            System.Diagnostics.Trace.WriteLine($"Hello from TestModule init!");
        }

        // Scrape method. Called whenever the framework recieves a scrape request and this module
        // matches the requested content.
        [ModuleScrape]
        public ModuleData? Scrape(ScrapeParameters parameters)
        {
            ModuleData data = new ModuleData();
            ProcessedScrapeData exampleInstance = new ProcessedScrapeData(ContentType.Text, parameters.URL, "Hello World!");
            data.Content.Add(exampleInstance);
            return data;
        }

        // Supplementary method. This is run every framework execution cycle in a separate thread
        // collectively with all other loaded module's background work. Essentially works as a
        // "tick" system.
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