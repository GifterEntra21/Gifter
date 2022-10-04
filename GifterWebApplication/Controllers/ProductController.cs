using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
using BusinessLogicalLayer;
using Entities;
using DataAccessLayer;

namespace GifterWebApplication.Controllers
{
    public class ProductController : Controller
    {
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

        [HttpPost("/Insert")]
        [ProducesResponseType(404)]
        //[Authorize]
        public async Task<IActionResult> InsertProduct(Product product)
        {
            try
            {
                ProductBLL productBLL = new();
                Response res = await productBLL.Insert(product);

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

        [HttpPut("/Update")]
        [ProducesResponseType(404)]
        //[Authorize]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {

                ProductBLL productBLL = new();
                Response res = await productBLL.Update(product);


                if (!res.HasSucess)
                {
                    return NotFound(res.Exception.Message);
                }

                return Ok("Atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/Delete")]
        [ProducesResponseType(404)]
        //[Authorize]
        public async Task<IActionResult> DeleteProduct(Product product)
        {
            try
            {
                ProductBLL productBLL = new();
                Response res = await productBLL.Delete(product);


                if (!res.HasSucess)
                {
                    return NotFound(res.Exception.Message);
                }

                return Ok("Deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
