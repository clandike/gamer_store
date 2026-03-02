using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace GamerStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Brand")]
        public int BrandId { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Model { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? ShortDescription { get; set; }

        public string? FullDescription { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public string ImageFileName { get; set; }

        public int IsInStock { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        [JsonIgnore]
        public Brand? Brand { get; set; }

        [JsonIgnore]
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
