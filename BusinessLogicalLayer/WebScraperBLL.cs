using DataAccessLayer;

namespace BusinessLogicalLayer
{
    public static class WebScraperBLL
    {
        public static List<string> Scrape()
        {
            return WebScraperDAL.ScrapeInstagramWithDefaultAccount(false, "neymarjr");
        }
    }
}