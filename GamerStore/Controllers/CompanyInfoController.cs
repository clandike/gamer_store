using Microsoft.AspNetCore.Mvc;

namespace GamerStore.Controllers
{
    [Controller]
    [Route("aboutUs")]
    public class CompanyInfoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
