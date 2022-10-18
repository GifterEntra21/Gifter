using Entities;
using GifterWebApplication.Models.Products;

namespace GifterWebApplication.Models.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductInsertViewModel, Product>();
            CreateMap<Product, ProductInsertViewModel>();

            CreateMap<ProductSelectViewModel,Product> ();
            CreateMap<Product, ProductSelectViewModel>();

            CreateMap<ProductDeleteViewModel, Product>();
            CreateMap<Product,ProductDeleteViewModel >();


            CreateMap<ProductUpdateViewModel, Product>();
            CreateMap<Product, ProductUpdateViewModel>();
        }
    }
}
