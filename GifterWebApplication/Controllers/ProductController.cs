using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
using Entities;
using BusinessLogicalLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GifterWebApplication.Controllers
{
    public class ProductController : Controller
    {

        public readonly IProductBLL _ProductService;

        public ProductController(IProductBLL productService)
        {
            _ProductService = productService;
        }

        [HttpGet("/ProductsByGenre")]
        [Authorize]
        public async Task<IActionResult> GetGifts(string genre)
        {
            DataResponse<Product> res = await _ProductService.GetByGenre(genre.ToLower());

            if (res.ItemList == null)
            {
                return NotFound();
            }

            return Ok(res.ItemList);
        }

        [HttpGet("/AllProducts")]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {

            try
            {
                DataResponse<Product> res = await _ProductService.GetAll();

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
        [Authorize]
        public async Task<IActionResult> InsertProduct(Product product)
        {
            try
            {
                
                Response res = await _ProductService.Insert(product);

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
        [Authorize]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {

               
                Response res = await _ProductService.Upsert(product);


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
        [Authorize]
        public async Task<IActionResult> DeleteProduct(Product product)
        {
            try
            {
               
                Response res = await _ProductService.Delete(product);


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
