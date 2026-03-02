using Microsoft.AspNetCore.Mvc;

namespace GamerStore.Controllers
{
    [Controller]
    [Route("equipmentTips")]
    public class EquipmentTipController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
