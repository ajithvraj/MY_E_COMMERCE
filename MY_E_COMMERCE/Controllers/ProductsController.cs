using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MY_E_COMMERCE.DTOs.ProductsDTOs;
using MY_E_COMMERCE.Services.ProductsServices;

namespace MY_E_COMMERCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductServices _productsService;

        public ProductsController(IProductServices productsService)
        {

            _productsService = productsService;

        }

        [HttpGet("products")]


        [Authorize(Roles = "Admin")]


        public async Task<IActionResult> GetProducts()
        {


            var products = await _productsService.GetaAllProdctsAsync();

            return Ok(products);


        }




        [HttpPut("product-status-flag")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateProductFlg([FromBody] ProductsStatusDTO model)
        {

             try
            {

                var result = await _productsService.UpdateProductStatus(model);

                //if (!result)
                //    return  NotFound(new { message = "produc not found or not updated " });
                

                return Ok(new { message = "product status updated successfully" });

            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

           catch (Exception ex)
            {

                return StatusCode(500, new { message = "internal server error" });

            }

        }

        [HttpPost("add-discount")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApplyDiscount([FromBody] DiscountDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _productsService.UpdateDiscount(model);

                if (result)
                    return Ok(new { Message = "Discount applied successfully." });

                return BadRequest(new { Message = "Discount application failed." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Optional: log exception internally
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }


        [HttpGet("search-product")]
        public async Task<IActionResult>SearchProduct([FromQuery] int? ProductId , string? ProductName)
        {
           // try
            //{

                var result = await _productsService.SearchProductAsync(ProductId, ProductName);

                if (result == null)
                
                    return BadRequest(new { Message = "product not found" });



                

                 return Ok(result);






           


           

        }

        [HttpGet("productsForUsers")]

        public async Task<IActionResult> UsersProducts()
        {

            var products = await _productsService.GetUsersProductsAsync();

            return Ok(products);

        }
















    }
}





    





    



