using ConsoleApp1;
using e_shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_shop.Components
{
    public class ProductCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Product product)
        {
            this.ViewBag.Product = product;

            return View();
        }
    }
}
