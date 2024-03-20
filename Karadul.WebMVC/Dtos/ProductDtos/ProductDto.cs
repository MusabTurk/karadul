namespace Karadul.WebMVC.Dtos.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
        public byte[] ProductPicture { get; set; }
        public bool HotTrend { get; set; }
        public bool BestSeller { get; set; }
        public bool Feature { get; set; }
    }
}
