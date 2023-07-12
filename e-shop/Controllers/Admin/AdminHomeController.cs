using Microsoft.AspNetCore.Mvc;

namespace e_shop.Controllers.Admin
{
    [Secured]
    public class AdminHomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
