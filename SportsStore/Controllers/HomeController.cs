using Microsoft.AspNetCore.Mvc;
using GamerStore.Data.Repository;

namespace GamerStore.Controllers
{
    [Controller]
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IStoreRepository repository;

        public HomeController(IStoreRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
