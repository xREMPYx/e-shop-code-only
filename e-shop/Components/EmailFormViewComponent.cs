using Microsoft.AspNetCore.Mvc;

namespace e_shop.Components
{
    public class EmailFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
