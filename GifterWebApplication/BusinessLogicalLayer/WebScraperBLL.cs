using DataAccessLayer;
using Shared.Resposes;
using System.Diagnostics;
using System.Threading.Tasks;
namespace BusinessLogicalLayer
{
    public static class WebScraperBLL
    {
        public static DataResponse<string> Scrape(bool showBrowser, string profile)
        {
            return WebScraperDAL.ScrapeInstagramWithDefaultAccount(showBrowser, profile);           
        }
    }
}