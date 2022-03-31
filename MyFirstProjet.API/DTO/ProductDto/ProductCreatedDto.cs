namespace MyFirstProject.API.DTO.ProductDto
{
    public class ProductCreatedDto
    {

        public string Name { get; set; }

        public double Price { get; set; }

        public double PromoPrice { get; set; }

        public Guid BrandId { get; set; }
    }
}
