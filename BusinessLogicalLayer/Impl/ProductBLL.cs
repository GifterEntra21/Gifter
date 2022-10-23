using BusinessLogicalLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Impl;
using DataAccessLayer.Interfaces;
using Entities;
using Entities.Interfaces;
using Shared.Responses;
using System.Configuration;
using StackExchange.Redis;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Drawing.Text;

namespace BusinessLogicalLayer.Impl
{
    public class ProductBLL : IProductBLL
    {

        public readonly IProductDAL _ProductService;
        public readonly IDistributedCache _Cache;

        public ProductBLL(IProductDAL productService, IDistributedCache cache)
        {
            _ProductService = productService;
            _Cache = cache;
        }

        public async Task<DataResponse<Product>> GetAll()
        {
            try
            {
                return await _ProductService.GetAll();

            }   
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(ex);
            }
        }

        public async Task<DataResponse<Product>> GetByAssociatedPartner(string AssociatedPartner)
        {
            try
            {

                return await _ProductService.GetByAssociatedPartner(AssociatedPartner);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(ex);
            }
        }
 
        public async Task<DataResponse<Product>> GetByGenre(string genre)
        {
            try
            {
                return await _ProductService.GetByGenre(genre);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(ex);
            }
        }

        public async Task<SingleResponse<Product>> GetById(string id)
        {
            try
            {
                return await _ProductService.GetById(id);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Product>(ex);
            }
        }

        public async Task<Response> Insert(Product product)
        {
            try
            {
           
                return await _ProductService.Insert(product);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<Response> Upsert(Product updatedProduct)
        {
            try
            {
                return await _ProductService.Upsert(updatedProduct);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
        public async Task<Response> Delete(Product product)
        {
            try
            {
                return await _ProductService.Delete(product);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }


        public async Task<Response> ClicksPlus(string _productId)
        {

            try
            {
                //Vai buscar no redis oq tem
                string productCache = await _Cache.GetStringAsync("KeyPadrao");

                Dictionary<string, int> clicksCache = JsonSerializer.Deserialize<Dictionary<string, int>>(productCache);

                //Vai desserializar o Json para um dicionario

                //Se o id informado ainda nao existir no redis é adicionado 
                if (!clicksCache.ContainsKey(_productId))
                {
                    clicksCache.Add(_productId, 1);
                }
                //se ele existe é incrementado 1
                else
                {
                    int qtdClicks = clicksCache[_productId];

                    clicksCache[_productId]++;
                    
                }
                //Serializado novamente 
                string ClicksNew = JsonSerializer.Serialize(clicksCache);

                //Setado novamente
                await _Cache.SetStringAsync("KeyPadrao", ClicksNew);

                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
    }
}
