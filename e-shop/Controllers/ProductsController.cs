using ConsoleApp1;
using e_shop.Enums;
using e_shop.Models;
using e_shop.Models.ViewModels;
using e_shop.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace e_shop.Controllers
{
    public class ProductsController : BaseController
	{
		private readonly MyContext _context;

		private readonly ProductsService _productsService;

		private readonly RatingService _ratingService;

		public ProductsController(MyContext context)
        {
			_context = context;
			_productsService = new ProductsService(context);
			_ratingService = new RatingService(context);
        }

		public IActionResult Index(int? categoryId, int? minPrice, int? maxPrice, int itemsCount = 8, int page = 0, ProductSortType type = ProductSortType.DEFAULT)
		{			
			FilterParameters parameters = new (_context, page, itemsCount, type, minPrice, maxPrice, categoryId);

			ProductsViewModel model = new ProductsViewModel()
			{
				MinPrice = parameters.MinPrice,
				MaxPrice = parameters.MaxPrice,
				Page = parameters.Page,
				SortType = parameters.SortType,
				ItemsCount = parameters.ItemsCount,
				Category = parameters.Category,
				DisplayProducts = _productsService.GetProducts(parameters)
			};

			return View(model);
		}

		public IActionResult Detail(int? id, int? colorId, int? sizeId)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var product =  _context.Products
				.FirstOrDefault(m => m.Id == id);

			if (product == null)
			{
				return NotFound();
			}

			this.ViewBag.AverageStars = _ratingService.GetAverageRating(product);

			List<Product> displayProducts = _productsService.GetRandomProducts(4);

			ProductDetailViewModel viewModel = new ProductDetailViewModel(colorId, sizeId, product, displayProducts, _context);

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult SendRating(ProductDetailViewModel model)
        {
			object routeValues = new
			{
				id = model.ProductId,
				colorId = model.SelectedColorId,
				sizeId = model.SelectedSizeId
			};

			Feedback feedback = model.NewFeedback;
			feedback.Product = _context.Products.Where(p => p.Id == model.ProductId).First();

			_context.Feedback.Add(feedback);
			_context.SaveChanges();

			return RedirectToAction("Detail", routeValues);
        }
	}
}
