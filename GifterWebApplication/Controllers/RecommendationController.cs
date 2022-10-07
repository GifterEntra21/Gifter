using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
using BusinessLogicalLayer;
using Entities;
using BusinessLogicalLayer.Impl;
using BusinessLogicalLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GiterWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RecommendationController : Controller
    {

        public readonly IWebScrapperBLL _WebScrapperService;

        public RecommendationController(IWebScrapperBLL webScrapperService)
        {
            _WebScrapperService = webScrapperService;
        }



        [HttpGet("/RecommendedGifts")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200, Type = typeof(DataResponse<Product>))]
        // Scrape the profile for images and recommend gifts based on that
        public async Task<IActionResult> GetGifts(string profile)
        {
            string _profile = profile.Replace("@", "");
            List<TagWithCount> tags = await _WebScrapperService.Scrape(_profile);
            DataResponse<Product> giftsResponse = await _WebScrapperService.GetGifts(tags, profile);
            List<Product> gifts = giftsResponse.ItemList;

            if (gifts == null)
            {
                return NotFound();
            }

            return Ok(gifts);
        }


        [HttpGet("/MostCommonTag")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200, Type = typeof(DataResponse<TagWithCount>))]
        public async Task<IActionResult> GetMostCommonTag(string user)
        {
            // response é a lista que contém todas as tags encontradas nas imagens do perfil, sendo que
            // elas foram transformadas em uma classe que possui como propriedades o nome e a quantidade de cada tag

            List<TagWithCount> response = await _WebScrapperService.Scrape(user);
            TagWithCount tag = response.MaxBy(t => t.Count);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(tag.Name);
        }

        [HttpGet("/AllTags")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200, Type = typeof(DataResponse<TagWithCount>))]
        public async Task<IActionResult> GetAllTags(string user)
        {

            List<TagWithCount> response = await _WebScrapperService.Scrape(user);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);


        }
    }
}
