using DataAccessLayer;
using Shared.Resposes;
using System.Diagnostics;
using System.Threading.Tasks;
namespace BusinessLogicalLayer
{
    public static class WebScraperBLL
    {
        static double mediatempo;
        static int i;

        public static DataResponse<string> Scrape(bool showBrowser, string profile)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            i++;
            var task = Task.Run(() => WebScraperDAL.ScrapeInstagramWithDefaultAccount(showBrowser, profile));
            if (task.Wait(TimeSpan.FromSeconds(double.PositiveInfinity)))
            {
                return task.Result;
               
                timer.Stop();

                TimeSpan timeSpan = timer.Elapsed;

                mediatempo = timeSpan.TotalSeconds;                
            }
            return new DataResponse<string>
            {
                Message = "Tempo Expirado",
                HasSucces = false,
                Item = null
            };
        }
    }
}