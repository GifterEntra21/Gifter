using Microsoft.AspNetCore.Mvc;
using GiterWebAPI.Helpers;
using Shared.Resposes;
using BusinessLogicalLayer;

namespace GiterWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WebScraperController : Controller
    {


        [HttpGet("/Pictures")]        
        [ProducesResponseType(404)]
        [Authorize]
        [ProducesResponseType(302, Type = typeof(DataResponse<string>))]
        public async Task<IActionResult> GetTags()
        {
            List<string> response = await WebScraperBLL.Scrape("vitor.fauste");

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

    }
}
