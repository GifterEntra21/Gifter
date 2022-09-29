using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
using BusinessLogicalLayer;
using Entities;
using DataAccessLayer;

namespace GifterWebApplication.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet("/Gifts222")]
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

        [HttpGet("/Insert")]
        [ProducesResponseType(404)]
        //[Authorize]
        public async Task<IActionResult> InsertProduct(Product product)
        {
            try
            {
                product.id = Guid.NewGuid().ToString();

                ProductDAL productDAL = new();
                Response res = await productDAL.Insert(product);


                if (!res.HasSucess)
                {
                    return NotFound(res.Exception.Message);
                }

                return Ok("Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
