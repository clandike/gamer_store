using GamerStore.Data.Repository;
using GamerStore.Models.Data;
using GamerStore.Models.DTO;
using GamerStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerStore.Controllers
{
    [Authorize]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IStoreRepository storeRepository;
        private readonly IOrderService orderService;
        private readonly IProductService productService;

        public AdminController(IStoreRepository storeRepository, IOrderService orderService, IProductService productService)
        {
            this.storeRepository = storeRepository;
            this.orderService = orderService;
            this.productService = productService;
        }

        [HttpGet]
        [Route("Orders")]
        public async Task<ViewResult> Orders()
        {
            var orders = await this.orderService.GetOrdersAsync();

            return this.View(orders);
        }

        [HttpGet]
        [Route("Products")]
        public async Task<ViewResult> Products() => this.View(await this.productService.GetProductsAsync());

        [HttpGet]
        [Route("Details/{productId:int}")]
        public async Task<ViewResult> Details(int productId)
        {
            ProductDTO product = new ProductDTO();

            if (this.ModelState.IsValid)
            {
                var products = await this.productService.GetProductsAsync();
                product = products.FirstOrDefault(p => p.Id == productId)!;
            }

            return this.View(product);
        }

        [HttpGet]
        [Route("Products/Edit/{productId:long}")]
        public async Task<ViewResult> Edit(int productId)
        {
            ProductDTO? product = null;

            if (this.ModelState.IsValid)
            {
                var products = await this.productService.GetProductsAsync();
                product = products.FirstOrDefault(p => p.Id == productId)!;
            }

            return this.View(product);
        }

        [HttpPost]
        [Route("Products/Edit/{productId:long}")]
        public async Task<IActionResult> Edit(ProductDTO productDto)
        {
            var product = new Product()
            {
                Id = productDto.Id,
                BrandId = productDto.BrandId,
                CategoryId = productDto.CategoryId,
                Model = productDto.Model,
                Title = productDto.Title,
                ShortDescription = productDto.ShortDescription,
                FullDescription = productDto.FullDescription,
                Price = productDto.Price,
                ImageFileName = productDto.ImageFileName,
                IsInStock = productDto.IsInStock,
            };
            await this.storeRepository.SaveProductAsync(product);
            return RedirectToAction("Products");
        }

        [HttpGet("Products/Create")]
        public ViewResult Create()
        {
            return this.View(new ProductDTO());
        }

        [HttpPost]
        [Route("Products/Create")]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            var product = new Product()
            {
                Id = productDto.Id,
                BrandId = productDto.BrandId,
                CategoryId = productDto.CategoryId,
                Model = productDto.Model,
                Title = productDto.Title,
                ShortDescription = productDto.ShortDescription,
                FullDescription = productDto.FullDescription,
                Price = productDto.Price,
                ImageFileName = productDto.ImageFileName,
                IsInStock = productDto.IsInStock,
            };

            await this.storeRepository.SaveProductAsync(product);
            return this.RedirectToAction("Products");
        }

        [HttpGet]
        [Route("Products/Delete/{productId:long}")]
        public async Task<IActionResult> Delete(int productId)
        {
            Product product = new Product();
            if (this.ModelState.IsValid)
            {
                var products = await this.storeRepository.GetProductsAsync();
                product = products.FirstOrDefault(p => p.Id == productId)!;
            }

            var model = new ProductDTO()
            {
                Id = product.Id,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Model = product.Model,
                Title = product.Title,
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                Price = product.Price,
                ImageFileName = product.ImageFileName,
                IsInStock = product.IsInStock
            };

            return this.View(model);
        }

        [HttpPost]
        [Route("Products/Delete/{productId:long}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (this.ModelState.IsValid)
            {
                var products = await this.storeRepository.GetProductsAsync();
                var product = products.FirstOrDefault(p => p.Id == productId);
                await this.storeRepository.DeleteProductAsync(product!);
            }

            return this.RedirectToAction("Products");
        }
    }
}
