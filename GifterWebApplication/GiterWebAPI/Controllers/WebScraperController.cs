using Microsoft.AspNetCore.Mvc;
using BusinessLogicalLayer;
using Shared.Resposes;
using GiterWebAPI.Helpers;
using GiterWebAPI.Interfaces;
using Microsoft.Extensions.Options;

namespace GiterWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WebScraperController : Controller
    {


        [HttpGet("/Pictures")]        
        [ProducesResponseType(404)]
        [ProducesResponseType(302, Type = typeof(DataResponse<string>))]
        public IActionResult GetWebScraper(string profile)
        {
            DataResponse<string> a = WebScraperBLL.Scrape(false, profile);

            if (!a.HasSucces || a.Item == null)
            {
                return NotFound();
            }

            return Ok(a.Item);
        }

    }
}
