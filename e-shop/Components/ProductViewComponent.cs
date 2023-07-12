using ConsoleApp1;
using e_shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_shop.Components
{
    public class ProductViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Product product)
        {
            return View(new ProductViewModel(product));
        }
    }
}
