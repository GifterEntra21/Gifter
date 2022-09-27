using Microsoft.AspNetCore.Mvc;
using GiterWebAPI.Helpers;
using Shared.Responses;
using BusinessLogicalLayer;
using Entities;

namespace GiterWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WebScraperController : Controller
    {


        [HttpGet("/Pictures")]        
        [ProducesResponseType(404)]
        //[Authorize]
        [ProducesResponseType(302, Type = typeof(DataResponse<string>))]
        public async Task<IActionResult> GetTags()
        {
            // response é a lista que contém todas as tags encontradas nas imagens do perfil, sendo que
            // elas foram transformadas em uma classe que possui como propriedades o nome e a quantidade de cada tag

            // trabalhe com a variável response conforme for necessário
            string user = "neymarjr";
            List<TagWithCount> response = await WebScraperBLL.Scrape(user);
            TagWithCount tag = response.MaxBy(t => t.Count);

            string retorno = $"The user @{user} likes: " + tag.Name;
            if (response == null)
            {
                return NotFound();
            }

            return Ok(retorno);
        }

        [HttpGet("/Gifts")]
        //[Authorize]
        public async Task<IActionResult> GetGifts(string profile)
        {

            List<TagWithCount> tags = await WebScraperBLL.Scrape(profile);
            List<Product> gifts = await WebScraperBLL.GetGifts(tags, profile);

            
            if (gifts == null)
            {
                return NotFound();
            }

            return Ok(gifts);
        }

    }
}
