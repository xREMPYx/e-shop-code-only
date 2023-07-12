using ConsoleApp1;
using e_shop.Enums;
using e_shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_shop.Controllers
{
    public class CheckoutController : BaseController
    {
        private readonly MyContext _context;

        public CheckoutController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Checkout_1()
        {
            this.ViewBag.OrderItems = this.GetCart(_context);

            this.ViewBag.CheckoutState = CheckoutState.INFORMATION;

            Order order = new Order()
            {
                CustomerDetails = new CustomerDetail(),
                OrderItems = this.GetCart(_context)
            };

            return View(order);
        }

        [HttpPost]
        public IActionResult Checkout_1(Order order)
        {
            this.ViewBag.OrderItems = this.GetCart(_context);

            this.ViewBag.CheckoutState = CheckoutState.INFORMATION;

            return View(order);
        }

        [HttpPost]
        public IActionResult Checkout_2(Order order)
        {
            this.ViewBag.OrderItems = this.GetCart(_context);

            this.ViewBag.CheckoutState = CheckoutState.SHIPPING;

            return View(order);
        }

        [HttpPost]
        public IActionResult Checkout_3(Order order)
        {
            this.ViewBag.OrderItems = this.GetCart(_context);

            this.ViewBag.CheckoutState = CheckoutState.PAYMENT;

            return View(order);
        }

        [HttpPost]
        public IActionResult Checkout_Complete(Order order)
        {
            List<OrderItem> items = this.GetCart(_context);

            this.ViewBag.OrderItems = items;

            order.Order_Date = DateTime.Now;
            order.OrderItems = items;
            order.OrderItems.ForEach(item =>
            {
                item.TotalTax = item.Variant.GetDiscountedPriceTax() * item.Quantity;
                item.TotalNoTax = item.Variant.GetDiscountedPriceNoTax() * item.Quantity;                
            });

            order.TotalTax = order.OrderItems.Select(i => i.TotalTax).Aggregate((a, b) => a + b);
            order.TotalNoTax = order.OrderItems.Select(i => i.TotalNoTax).Aggregate((a, b) => a + b);

            ReduceStockUnits(order);

            this._context.Orders.Add(order);
            this._context.SaveChanges();

            this.RemoveCart();

            return View(order);
        }

        private void ReduceStockUnits(Order order)
        {
            foreach (OrderItem item in order.OrderItems)
            {
                Variant variant = item.Variant;

                variant.Stock -= item.Quantity;
                
                _context.Variants.Update(variant);
            }
        }
    }
}
