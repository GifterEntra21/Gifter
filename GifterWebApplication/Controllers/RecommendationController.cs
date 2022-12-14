using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
using BusinessLogicalLayer;
using Entities;
using BusinessLogicalLayer.Impl;
using BusinessLogicalLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GiterWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RecommendationController : ControllerBase
    {

        public readonly IWebScrapperBLL _WebScrapperService;

        public RecommendationController(IWebScrapperBLL webScrapperService)
        {
            _WebScrapperService = webScrapperService;
        }


        [HttpGet]
        [Route("RecommendGifts")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200, Type = typeof(List<Product>))]
        // Scrape the profile for images and recommend gifts based on that
        public async Task<IActionResult> GetGifts([FromQuery] string request) 
        {
            string _profile = request.Replace("@", "").Replace(" ","");

            DataResponse<Product> tags = await _WebScrapperService.VerifyProfile(_profile);
            if (!tags.HasSuccess)
            {
                return NotFound(tags.Exception);
            }
            
            List<Product> gifts = tags.ItemList;
            return Ok(gifts);
        }


        [HttpGet]
        [Route("GetMostCommonTag")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200, Type = typeof(List<TagWithCount>))]
        public async Task<IActionResult> GetMostCommonTag([FromQuery] string request) 
        {
            // response é a lista que contém todas as tags encontradas nas imagens do perfil, sendo que
            // elas foram transformadas em uma classe que possui como propriedades o nome e a quantidade de cada tag

            DataResponse<TagWithCount> response = await _WebScrapperService.Scrape(request);
            TagWithCount tag = response.ItemList.MaxBy(t => t.Count);

            if (!response.HasSuccess)
            {
                return NotFound();
            }

            return Ok(tag.Name);
        }

        [HttpGet]
        [Route("GetAllTags")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200, Type = typeof(List<TagWithCount>))]
        public async Task<IActionResult> GetAllTags([FromQuery] string request)
        {

            DataResponse<TagWithCount> response = await _WebScrapperService.Scrape(request);

            if (!response.HasSuccess)
            {
                return NotFound();
            }
            return Ok(response);

        }
    }
}
