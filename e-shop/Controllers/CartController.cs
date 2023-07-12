using ConsoleApp1;
using e_shop.Models;
using e_shop.Models.DataModels;
using e_shop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace e_shop.Controllers
{
    public class CartController : BaseController
    {
        private readonly MyContext _context;

        public CartController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<OrderItem> cart = this.GetCart(_context);

            return View(cart);
        }

        public IActionResult Add(int productId, int colorId, int sizeId)
        {
            List<Variant> variants = _context.Variants.Where(p => p.Product.Id == productId).ToList();

            Variant variant = variants.Where(v => v.Color.Id == colorId && v.Size.Id == sizeId).FirstOrDefault();

            if (variant == null)
            {
                return Problem("Error");
            }

            List<OrderItemDataModel> cart = this.ViewBag.Cart;

            if (cart.Any(item => item.VariantId == variant.Id))
            {
                OrderItemDataModel orderItem = cart.Where(item => item.VariantId == variant.Id).First();

                orderItem.Quantity++;
            }
            else
            {
                cart.Add(new OrderItemDataModel()
                {
                    VariantId = variant.Id,
                    Quantity = 1
                });
            }            

            SaveCart(cart);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int index, int quantity)
		{
            List<OrderItemDataModel> cart = this.ViewBag.Cart;

            if (index < cart.Count() && index >= 0)
            {
                cart[index].Quantity = quantity <= 0 ? 1 : quantity;
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int index)
        {
            List<OrderItemDataModel> cart = this.ViewBag.Cart;

            if (index < cart.Count() && index >= 0)
            {
                cart.RemoveAt(index);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        private void SaveCart(List<OrderItemDataModel> cart)
        {
            this.HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
        }
    }
}
