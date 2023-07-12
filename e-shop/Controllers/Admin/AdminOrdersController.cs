using e_shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_shop.Controllers.Admin
{
    [Secured]
    public class AdminOrdersController : BaseController
    {
        private readonly MyContext _context;

        public AdminOrdersController(MyContext context)
        {
            _context = context;
        }

        // GET: AdminOrders
        public IActionResult Index()
        {
            return View(_context.Orders.ToList());
        }

        // GET: AdminOrders/Details/5
        public IActionResult Detail(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = _context.Orders
                .FirstOrDefault(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
