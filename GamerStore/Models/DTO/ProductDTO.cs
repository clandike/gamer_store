namespace GamerStore.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string? Model { get; set; }

        public string? Title { get; set; }

        public string? ShortDescription { get; set; }

        public string? FullDescription { get; set; }

        public decimal Price { get; set; }

        public string ImageFileName { get; set; }

        public int IsInStock { get; set; }
    }
}
