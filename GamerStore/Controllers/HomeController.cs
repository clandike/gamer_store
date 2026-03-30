using Microsoft.AspNetCore.Mvc;
using GamerStore.Data.Repository;

namespace GamerStore.Controllers
{
    [Controller]
    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
