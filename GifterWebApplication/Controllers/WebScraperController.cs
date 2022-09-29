using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
using BusinessLogicalLayer;
using Entities;

namespace GiterWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WebScraperController : Controller
    {


        [HttpGet("/MostCommonTag")]        
        [ProducesResponseType(404)]
        //[Authorize]
        [ProducesResponseType(302, Type = typeof(DataResponse<string>))]
        public async Task<IActionResult> GetTags(string user)
        {
            // response é a lista que contém todas as tags encontradas nas imagens do perfil, sendo que
            // elas foram transformadas em uma classe que possui como propriedades o nome e a quantidade de cada tag

            List<TagWithCount> response = await WebScraperBLL.Scrape(user);
            TagWithCount tag = response.MaxBy(t => t.Count);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(tag.Name);
        }

        [HttpGet("/Gifts")]
        //[Authorize]
        public async Task<IActionResult> GetGifts(string profile)
        {

            List<TagWithCount> tags = await WebScraperBLL.Scrape(profile);
            DataResponse<Product> giftsResponse = await WebScraperBLL.GetGifts(tags, profile);
            List<Product> gifts = giftsResponse.Item;

            
            if (gifts == null)
            {
                return NotFound();
            }

            return Ok(gifts);
        }

    }
}
