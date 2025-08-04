using Dapper;
using MY_E_COMMERCE.DataAccess;
using MY_E_COMMERCE.DTOs.ProductsDTOs;
using MY_E_COMMERCE.Model;
using System.Data;

namespace MY_E_COMMERCE.Repositories.ProductsRepo
{
    public class ProductRepository : IProductRepository
    {

        public readonly ISqlConnectionFactory _connectionFactory; 

        public ProductRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        public async Task<IEnumerable<ProductDto>> GetaAllProdctsAsync()
        {

         using   var connection = _connectionFactory.CreateConnection();

            var products = await connection.QueryAsync<ProductDto>(


                "sp_GetAllProducts", commandType: CommandType.StoredProcedure


                );

            return products;



        }


        public async Task<bool> UpdateProductStatus(ProductsStatusDTO model)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameter = new DynamicParameters();
            parameter.Add("@ProductId",model.Product_Id);
            parameter.Add("@IsAvailable",model.ISAVAILABLE);
            parameter.Add("@IsDeleted", model.IS_DELETED);

            var rowsAffected = await connection.ExecuteAsync("sp_DELETEPRODUCT",

                parameter, commandType: CommandType.StoredProcedure

                );

            return rowsAffected > 0;
        }



        public async Task<bool> UpdateDiscount(decimal Discount_Percentage)
        {

            var connection = _connectionFactory.CreateConnection();
            var parameter = new DynamicParameters();
            parameter.Add("@Discount_Price", Discount_Percentage );

            var rowsAffected = await connection.ExecuteAsync("sp_UPDATE_DISCOUNT",

                parameter, commandType: CommandType.StoredProcedure

                );

            return rowsAffected > 0;

        }

        public async Task<SearchProductDTO?> SearchProductAsync(int? ProductId, string? ProductName)
        {
            //if (ProductId == null || string.IsNullOrWhiteSpace(ProductName))
            //    throw new ArgumentException("Either ProductId or ProductName must be provided.");

            using var connection = _connectionFactory.CreateConnection();

            var parameter = new DynamicParameters();
            parameter.Add("@ProductName", ProductName);
            parameter.Add("@ProductId",ProductId);

            var rowAffected = await connection.QueryFirstOrDefaultAsync<SearchProductDTO>("sp_GET_PRODUCT_BY_ID_OR_NAME", 
                parameter , commandType: CommandType.StoredProcedure

                );


            return rowAffected;
        }

        public async Task<IEnumerable<SearchProductDTO>> GetUsersProductsAsync()
        {

            using var connection = _connectionFactory.CreateConnection();

            var products = await connection.QueryAsync<SearchProductDTO>(

                "sp_PRODUCT_USERS", commandType: CommandType.StoredProcedure




                );

            return products;
            

        }



    }
}
