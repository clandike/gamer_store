using System.ComponentModel.DataAnnotations;

namespace GamerStore.Models
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Product>? Products { get; }
    }
}
