using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerStore.Models.Data
{
    public class ProductImage
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string ImagePath { get; set; }

        public Product Product { get; set; }
    }
}
