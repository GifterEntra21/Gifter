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
        public async Task<IActionResult> GetMostCommonTag(string user)
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

        [HttpGet("/AllTags")]
        [ProducesResponseType(404)]
        //[Authorize]
        [ProducesResponseType(302, Type = typeof(DataResponse<string>))]
        public async Task<IActionResult> GetAllTags(string user)
        {
            try
            {

                List<TagWithCount> response = await WebScraperBLL.Scrape(user);

                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500,ResponseFactory.CreateInstance().CreateFailedResponse(ex));

            }
        }
    }
}
