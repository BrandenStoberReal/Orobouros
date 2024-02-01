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
    }
}