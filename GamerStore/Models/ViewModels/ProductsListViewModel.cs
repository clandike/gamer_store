using GamerStore.Models.DTO;

namespace GamerStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<ProductDTO> Products { get; set; } = Enumerable.Empty<ProductDTO>();

        public PagingInfo PagingInfo { get; set; } = new();

        public string? CurrentCategory { get; set; }
    }
}
