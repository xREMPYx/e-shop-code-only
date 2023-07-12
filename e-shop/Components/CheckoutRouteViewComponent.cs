using e_shop.Enums;
using Microsoft.AspNetCore.Mvc;

namespace e_shop.Components
{
    public class CheckoutRouteViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CheckoutState state)
        {
            this.ViewBag.CheckoutState = state;

            return View();
        }
    }
}
