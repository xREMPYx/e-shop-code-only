using ConsoleApp1;
using e_shop.Enums;

namespace e_shop.Models
{
    public class ProductsViewModel
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int ItemsCount { get; set; }
        public int Page { get; set; }
        public ProductSortType SortType { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Product> DisplayProducts { get; set; }
    }
}
