using e_shop.Models;
using e_shop.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace e_shop.Controllers
{
    public abstract class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            this.ViewBag.Authenticated = this.HttpContext.Session.GetString("login") != null;

            string? cartJson = this.HttpContext.Session.GetString("cart");

            this.ViewBag.Cart = cartJson == null 
                ? new List<OrderItemDataModel>() 
                : JsonConvert.DeserializeObject<List<OrderItemDataModel>>(cartJson);
        }

        public List<OrderItem> GetCart(MyContext contex)
		{
            string? cartJson = this.HttpContext.Session.GetString("cart");

            if (cartJson == null)
			{
                return new List<OrderItem>();
			}
			else
			{
                List<OrderItemDataModel> cart = JsonConvert.DeserializeObject<List<OrderItemDataModel>>(cartJson);

                return cart.Select(o => new OrderItem()
                {
                    Quantity = o.Quantity,
                    Variant = contex.Variants.Find(o.VariantId)
                }).ToList();
            }
		}

        public void RemoveCart()
        {
            this.HttpContext.Session.Remove("cart");
        }
    }
}
