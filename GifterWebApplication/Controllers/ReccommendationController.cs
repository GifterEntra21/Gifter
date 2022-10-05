﻿using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
using BusinessLogicalLayer;
using Entities;

namespace GiterWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReccommendationController : Controller
    {
        /// <summary>
        /// Scrape the profile for images and recommend gifts based on that
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        [HttpGet("/RecommendedGifts")]
        //[Authorize]
        public async Task<IActionResult> GetGifts(string profile)
        {

            List<TagWithCount> tags = await WebScraperBLL.Scrape(profile);
            DataResponse<Product> giftsResponse = await WebScraperBLL.GetGifts(tags, profile);
            List<Product> gifts = giftsResponse.ItemList;


            if (gifts == null)
            {
                return NotFound();
            }

            return Ok(gifts);
        }


            [HttpGet("/MostCommonTag")]
            [ProducesResponseType(404)]
            //[Authorize]
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
                    return StatusCode(500, ResponseFactory.CreateInstance().CreateFailedResponse(ex));

                }
            }
        }
    }
