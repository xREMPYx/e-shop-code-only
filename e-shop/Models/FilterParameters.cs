using ConsoleApp1;
using e_shop.Enums;

namespace e_shop.Models
{
    public struct FilterParameters
    {
        public int ItemsCount { get; set; }
        public int Page { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public ProductSortType SortType { get; set; }
        public Category Category { get; set; }

        public FilterParameters(MyContext context, int page, int itemsCount, ProductSortType type, int? minPrice, int? maxPrice, int? categoryId)
        {
            int minPriceAll = context.Products.Min(p => p.Variants.First().Price);
            int maxPriceAll = context.Products.Max(p => p.Variants.First().Price);

            minPrice = minPrice == null || minPrice < minPriceAll ? minPriceAll : minPrice;
            maxPrice = maxPrice == null || maxPrice > maxPriceAll ? maxPriceAll : maxPrice;

            ICollection<Product> products = context.Products.ToList();

            products = products.ToList().Where(p => p.Variants.First().GetDiscountedPriceTax() < maxPrice).ToList();
            products = products.ToList().Where(p => p.Variants.First().GetDiscountedPriceTax() > minPrice).ToList();

            int maxPage = (products.Count() / itemsCount);
            maxPage = (products.Count() % itemsCount) == 0 ? maxPage - 1 : maxPage;

            page = page > maxPage ? maxPage : page;
            page = page < 0 ? 0 : page;

            if (categoryId == null)
            {
                Category = new Category()
                {
                    Id = categoryId,
                    Name = String.Empty,
                    SubCategories = context.Categories.ToList()
                    .Where(c => c.Parent == null)
                    .ToList()
                };                
            }
            else
            {
                Category = context.Categories.Where(c => c.Id == categoryId).ToList().FirstOrDefault();
            }

            ItemsCount = itemsCount;

            Page = page;
            SortType = type;

            MinPrice = (int)minPrice;
            MaxPrice = (int)maxPrice;
        }

        public bool IsInCategory() => Category.Id != null;
    }
}
