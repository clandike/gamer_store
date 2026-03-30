namespace GamerStore.Models.Data
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Product>? Products { get; }
    }
}
