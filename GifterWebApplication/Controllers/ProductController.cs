using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
using BusinessLogicalLayer;
using Entities;
using DataAccessLayer;

namespace GifterWebApplication.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet("/ProductsByGenre")]
        //[Authorize]
        public async Task<IActionResult> GetGifts(string genre)
        {

            ProductBLL productBLL = new();
            DataResponse<Product> res = await productBLL.GetByGenre(genre.ToLower());


            if (res.ItemList == null)
            {
                return NotFound();
            }

            return Ok(res.ItemList);
        }

        [HttpGet("/AllProducts")]
        //[Authorize]
        public async Task<IActionResult> GetAllProducts()
        {

            try
            {
                ProductBLL productBLL = new();
                DataResponse<Product> res = await productBLL.GetAll();

                if (!res.HasSuccess)
                {
                    return NotFound(res.Exception.Message);
                }

                return Ok(res.ItemList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

                if (!res.HasSuccess)
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

        [HttpPut("/Upsert")]
        [ProducesResponseType(404)]
        //[Authorize]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {

                ProductBLL productBLL = new();
                Response res = await productBLL.Upsert(product);


                if (!res.HasSuccess)
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


                if (!res.HasSuccess)
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
