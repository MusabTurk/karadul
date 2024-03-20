namespace Karadul.WebAPI.Models.ProductModels
{
    public class UpdateProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }
        public bool HotTrend { get; set; }
        public bool BestSeller { get; set; }
        public bool Feature { get; set; }
        public byte[]? ProductPicture { get; set; }
        public int CategoryId { get; set; }

    }
}
