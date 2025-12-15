using restatunt.Shared.DTOs.Products;

namespace Orders.Application.DTOs
{
    public class DetailsOrderDto
    {
        public required string IdOrder { get; set; }
        public required string NameCustomer { get; set; }
        public required List<ProductDto> Products { get; set; }
        public required decimal Total { get; set; }
    }
}
