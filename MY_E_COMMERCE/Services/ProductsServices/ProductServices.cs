using Microsoft.AspNetCore.Http.HttpResults;
using MY_E_COMMERCE.DTOs.ProductsDTOs;
using MY_E_COMMERCE.Repositories.ProductsRepo;

namespace MY_E_COMMERCE.Services.ProductsServices
{
    public class ProductServices : IProductServices
    {

        private readonly IProductRepository _productRepository;
        private readonly IConfiguration _configuration;

        public ProductServices(IProductRepository productRepository , IConfiguration configuration)
        {
            _productRepository = productRepository;
            _configuration = configuration;

        }

        public async Task<IEnumerable<ProductDto>> GetaAllProdctsAsync()
        {

            return await _productRepository.GetaAllProdctsAsync();


        }

       public async Task<bool> UpdateProductStatus(ProductsStatusDTO model)
        {
            return await _productRepository.UpdateProductStatus(model);
        }


        public async Task<bool> UpdateDiscount(DiscountDTO model)
        {

            if (model.Discount_Percentage < 0 || model.Discount_Percentage > 100)

                throw new ArgumentException("the discount must be between 0 and 100");


            return await _productRepository.UpdateDiscount(model.Discount_Percentage);
        }


        public async Task<SearchProductDTO?> SearchProductAsync(int? Id, string? Name)
        {
            //if(Id == null || string.IsNullOrEmpty(Name))
            //    throw new ArgumentNullException("Provide either Id or Name of the product");



           

            return await _productRepository.SearchProductAsync(Id, Name);


        }

        public async Task<IEnumerable<SearchProductDTO>> GetUsersProductsAsync(){

            return await _productRepository.GetUsersProductsAsync();

        }














    }
}
