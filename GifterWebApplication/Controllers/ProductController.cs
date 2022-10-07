using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
using Entities;
using BusinessLogicalLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using GifterWebApplication.Models.Products;
using AutoMapper;

namespace GifterWebApplication.Controllers
{
    public class ProductController : Controller
    {

        public readonly IProductBLL _ProductService;
        private readonly IMapper _mapper;
        public ProductController(IProductBLL productService)
        {
            _ProductService = productService;
        }

        [HttpGet("/ProductsByGenre")]
        [ProducesResponseType(200, Type = typeof(DataResponse<Product>))]
        [Authorize]
        public async Task<IActionResult> GetGifts(string genre)
        {

            DataResponse<Product> res = await _ProductService.GetByGenre(genre.ToLower());

            List<ProductSelectViewModel> products = _mapper.Map<List<ProductSelectViewModel>>(res.ItemList);

            if (res.ItemList == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("/AllProducts")]
        [ProducesResponseType(200, Type = typeof(DataResponse<Product>))]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            DataResponse<Product> res = await _ProductService.GetAll();

            if (!res.HasSuccess)
            {
                return NotFound(res.Exception.Message);
            }

            return Ok(res.ItemList);


        }

        [HttpPost("/Insert")]
        [ProducesResponseType(201, Type = typeof(Response))]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> InsertProduct(Product product)
        {

            Response res = await _ProductService.Insert(product);

            if (!res.HasSuccess)
            {
                return NotFound(res.Exception.Message);
            }

            return Created("Cadastrado com sucesso.", res);

        }

        [HttpPut("/Upsert")]
        [ProducesResponseType(201, Type = typeof(Response))]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            Response res = await _ProductService.Upsert(product);
            if (!res.HasSuccess)
            {
                return NotFound(res.Exception.Message);
            }

            return Created("Atualizado com sucesso.", res);

        }

        [HttpDelete("/Delete")]
        [ProducesResponseType(200, Type = typeof(Response))]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteProduct(Product product)
        {
            Response res = await _ProductService.Delete(product);

            if (!res.HasSuccess)
            {
                return NotFound(res.Exception.Message);
            }

            return Ok("Deletado com sucesso.");

        }
    }
}
