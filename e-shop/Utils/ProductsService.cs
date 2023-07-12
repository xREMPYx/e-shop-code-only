using ConsoleApp1;
using e_shop.Enums;
using e_shop.Models;

namespace e_shop.Utils
{
    public class ProductsService
    {
        private readonly MyContext _context;

        private Dictionary<ProductSortType, Action<List<Product>>> _sortFunctions;

        public ProductsService(MyContext context)
        {
            _context = context;

            _sortFunctions = new()
            {
                [ProductSortType.PRICE_ASC] = (x) => x.Sort((a, b) => Price(a).CompareTo(Price(b))),
                [ProductSortType.PRICE_DESC] = (x) => x.Sort((a, b) => Price(b).CompareTo(Price(a))),
                [ProductSortType.TITLE_ASC] = (x) => x.Sort((a, b) => a.Title.CompareTo(b.Title)),
                [ProductSortType.TITLE_DESC] = (x) => x.Sort((a, b) => b.Title.CompareTo(a.Title))
            };
        }

        public List<Product> GetRandomProducts(int count)
        {
            MyRandom rng = new MyRandom();

            List<Product> products = _context.Products.ToList();

            List<Product> result = Enumerable.Range(0, count)
                .Select(p => products[rng.Next(0, products.Count())])
                .ToList();

            return result;
        }

        public List<Product> GetProducts(FilterParameters parameters)
        {
            List<Product> products = GetProducts(parameters.SortType);

            if (parameters.IsInCategory())
            {
                Category category = _context.Categories.Find(parameters.Category.Id);

                var categories = category.GetAllChildren();
                
                products = products.Where(p => categories.Contains(p.Category)).ToList();
            }

            products = products.Where(p => Price(p) < parameters.MaxPrice).ToList();
            products = products.Where(p => Price(p) > parameters.MinPrice).ToList();

            int start = parameters.ItemsCount * parameters.Page;

            int itemsCount = products.Count() - (parameters.ItemsCount * parameters.Page) > parameters.ItemsCount
                ? parameters.ItemsCount
                : products.Count() - (parameters.ItemsCount * parameters.Page);

            itemsCount = itemsCount < 0 ? 0 : itemsCount;

            if (itemsCount == 0)
            {
                return new List<Product>();
            }
            else
            {
                return products.GetRange(start, itemsCount);
            }
        }

        private List<Product> GetProducts(ProductSortType sortType)
        {
            List<Product> products = _context.Products.ToList();
            
            if (_sortFunctions.ContainsKey(sortType))
            {
                _sortFunctions[sortType](products);
            }

            return products;
        }

        private int Price(Product product)
        {
            Variant variant = product.Variants.First();

            return (int)(variant.Price * (1 - (variant.Discount * 0.01)));
        }
    }
}
