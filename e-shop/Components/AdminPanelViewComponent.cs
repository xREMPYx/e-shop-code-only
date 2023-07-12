using Microsoft.AspNetCore.Mvc;

namespace e_shop.Components
{
    public class AdminPanelViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
