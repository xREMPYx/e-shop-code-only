using ConsoleApp1;
using e_shop.Models;
using e_shop.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace e_shop.Controllers
{
    public class HomeController : BaseController
    {
        private readonly MyContext _context;

        private readonly ILogger<HomeController> _logger;

        private readonly ProductsService _service;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
            _service = new ProductsService(context);
        }

        public IActionResult Index()
        {
            List<Product> displayProducts = _service.GetRandomProducts(8);

            this.ViewBag.DisplayProducts = displayProducts;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}