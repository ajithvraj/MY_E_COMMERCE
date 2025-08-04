using MY_E_COMMERCE.DTOs.ProductsDTOs;

namespace MY_E_COMMERCE.Services.ProductsServices
{
    public interface IProductServices
    {

       public Task<IEnumerable<ProductDto>>GetaAllProdctsAsync();
       public Task<bool> UpdateProductStatus(ProductsStatusDTO model);
        public Task<bool> UpdateDiscount(DiscountDTO model);

        public Task<SearchProductDTO?> SearchProductAsync(int? Id, string? Name);

        public Task<IEnumerable<SearchProductDTO>> GetUsersProductsAsync();





    }
}
