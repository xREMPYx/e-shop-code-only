using Microsoft.AspNetCore.Mvc;

namespace e_shop.Controllers
{
    public class FeedbackController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}