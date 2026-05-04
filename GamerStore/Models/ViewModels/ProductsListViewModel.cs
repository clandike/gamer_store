using GamerStore.Models.DTO;

namespace GamerStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<ProductDTO> Products { get; set; } = Enumerable.Empty<ProductDTO>();

        public PagingInfo PagingInfo { get; set; } = new();

        public string? CurrentCategory { get; set; }

        public string[] SelectedBrands { get; set; }
        
        public string? SearchText { get; set; }

        public string? Sort { get; set; }
    }
}
