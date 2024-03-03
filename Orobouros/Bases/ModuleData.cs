namespace Orobouros.Bases
{
    /// <summary>
    /// Primary returned data class for any modules.
    /// </summary>
    public class ModuleData
    {
        /// <summary>
        /// Module associated with the returned data.
        /// </summary>
        public Module Module { get; set; }

        /// <summary>
        /// Date this data was requsted.
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// An array of data returned by the module.
        /// </summary>
        public List<ProcessedScrapeData> Content { get; set; } = new List<ProcessedScrapeData>();

        /// <summary>
        /// Class constructor.
        /// </summary>
        public ModuleData()
        {
            RequestDate = DateTime.Now;
        }
    }
}