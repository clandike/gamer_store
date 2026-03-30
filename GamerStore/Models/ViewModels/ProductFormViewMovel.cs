using GamerStore.Models.Data;

namespace GamerStore.Models.ViewModels
{
    public class ProductFormViewModel
    {
        public Product Product { get; set; } = new Product
        {
            Brand = new Brand(),
            Category = new Category()
        };
        public string ThemeColor { get; set; }
        public string TitleText { get; set; }
        public string CallbackMethodName { get; set; }
    }
}
