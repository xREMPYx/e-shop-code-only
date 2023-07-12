using Microsoft.AspNetCore.Mvc;

namespace e_shop.Components
{
    public class FeaturesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
