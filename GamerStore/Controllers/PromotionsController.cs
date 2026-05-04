using GamerStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerStore.Controllers
{
    [Route("promotions")]
    public class PromotionsController : Controller
    {
        private readonly IPromotionsService service;

        public PromotionsController(IPromotionsService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await this.service.GetPromoAsync();

            return this.View(data);
        }
    }
}
