using Microsoft.AspNetCore.Mvc;
using GiterWebAPI.Helpers;
using Shared.Responses;
using BusinessLogicalLayer;
using Microsoft.AspNetCore.Authorization;

namespace GiterWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WebScraperController : Controller
    {


        [HttpGet("/Pictures")]        
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetTags()
        {
            List<string> response = await WebScraperBLL.Scrape("neymarjr");

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

    }
}
