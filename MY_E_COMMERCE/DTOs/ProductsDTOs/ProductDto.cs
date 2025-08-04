namespace MY_E_COMMERCE.DTOs.ProductsDTOs
{
    public class ProductDto
    {

        public int Product_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int Category_Id { get; set; }
        public decimal Price { get; set; }
        public decimal Discount_Price { get; set; }
        public int Stock_Quantity { get; set; }
        public decimal Rating { get; set; }
        public bool IsAvailable { get; set; }
        public bool Is_Deleted { get; set; }
        public DateTime Created_At { get; set; }

    }
}
