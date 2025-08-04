using MY_E_COMMERCE.DTOs.ProductsDTOs;

namespace MY_E_COMMERCE.Repositories.ProductsRepo
{
    public interface IProductRepository
    {

        public Task<IEnumerable<ProductDto>> GetaAllProdctsAsync();
        public Task<bool>UpdateProductStatus(ProductsStatusDTO model);
        public Task<bool> UpdateDiscount(decimal Discount_Percentage);
        public Task<SearchProductDTO?> SearchProductAsync(int? ProductId , string? ProductName);
        public Task<IEnumerable<SearchProductDTO>> GetUsersProductsAsync();

    }
}
