using AutoMapper;
using BusinessLogicalLayer.Interfaces;
using Entities;
using GifterWebApplication.Models.Products;
using GifterWebApplication.Models.RecommendationRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace GifterWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductBLL _ProductService;
        private readonly IMapper _mapper;

        public ProductController(IProductBLL productService, IMapper mapper)
        {
            _ProductService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Product))]
        [Authorize]
        [Route("ProductsByGenre")]
        
        public async Task<IActionResult> GetGifts([FromQuery]DefaultRequest genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }
            DataResponse<Product> res = await _ProductService.GetByGenre(genre.Request.ToLower());

            List<ProductSelectViewModel> products = _mapper.Map<List<ProductSelectViewModel>>(res.ItemList);

            if (res.ItemList == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Product))]
        [Authorize]
        [Route("AllProducts")]

        public async Task<IActionResult> GetAllProducts()
        {
            DataResponse<Product> res = await _ProductService.GetAll();
            List<ProductSelectViewModel> products = _mapper.Map<List<ProductSelectViewModel>>(res.ItemList);

            if (!res.HasSuccess)
            {
                return NotFound(res.Exception.Message);
            }

            return Ok(products);
            


        }

        [HttpPost]
        [Route("Insert")]
        [ProducesResponseType(201)]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> InsertProduct([FromBody]ProductInsertViewModel product)
        {
            Product _product = _mapper.Map<Product>(product);
            Response res = await _ProductService.Insert(_product);

            if (!res.HasSuccess)
            {
                return NotFound(res.Exception.Message);
            }

            return Created("Cadastrado com sucesso.", res);

        }

        [HttpPut]
        [Route("Upsert")]
        [ProducesResponseType(201)]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateViewModel product)
        {
            Product _product = _mapper.Map<Product>(product);
            Response res = await _ProductService.Upsert(_product);
            if (!res.HasSuccess)
            {
                return NotFound(res.Exception.Message);
            }

            return Created("Atualizado com sucesso.", res.HasSuccess);

        }

        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteProduct([FromBody] ProductDeleteViewModel product)
        {
            Product _product = _mapper.Map<Product>(product);
            Response res = await _ProductService.Delete(_product);

            if (!res.HasSuccess)
            {
                return NotFound(res.Exception.Message);
            }

            return Ok("Deletado com sucesso.");

        }
    }
}
