using Microsoft.AspNetCore.Mvc;

namespace e_shop.Controllers.Admin
{
    [Secured]
    public class AdminFeedbackController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
