using UniScraperDLL.Bases;
using static UniScraperDLL.UniAssemblyInfo;

namespace UniScraper.TestModule
{
    // Module must inherit ScraperInfo.
    public class MainModule : ScraperInfo
    {
        // Module information. Must be in the module and names must not be changed. Values can be
        // freely modified.

        public override string Name { get; set; } = "Test Module";
        public override string Description { get; set; } = "A simple test module";
        public override string ModuleVersion { get; set; } = "1.0.0.0";

        public override List<string> SupportedWebsites { get; set; } = new List<string>
        {
            "https://www.test.com",
            "https://www.test2.com"
        };

        public override List<ScraperContent> SupportedContent { get; set; } = new List<ScraperContent>
        {
            ScraperContent.Text
        };

        // Module methods. Must follow specific naming guides

        // Initializer method. Must have no parameters and must return void.
        public void Initialize()
        {
            System.Diagnostics.Trace.WriteLine($"Hello from TestModule init!");
        }

        // Scrape method. MUST have only 2 parameters of the below types. Return type MUST be ScraperModuleData.
        public ScraperModuleData Scrape(string website, int numberOfContentInstances)
        {
            ScraperModuleData data = new ScraperModuleData();
            data.Website = website;
            data.RequestedDataAmount = numberOfContentInstances;
            data.ContentType = ScraperContent.Text;

            // Scrape data here
            data.Content.Add("Hello World!");

            // Return class
            return data;
        }
    }
}