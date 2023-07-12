using ConsoleApp1;

namespace e_shop.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? PhotoName { get; set; }
        public int SalePrice { get; set; }
        public int OriginalPrice { get; set; }
		public int ColorId { get; set; }
		public int SizeId { get; set; }

		public ProductViewModel(Product product)
        {            
            this.Id = product.Id;
            this.Title = product.Title;
            this.PhotoName = product.Photos != null && product.Photos.Count() > 0 ? product.Photos.MaxBy(p => p.Priority).Name : null;

            Variant variant = product.Variants.First();

            this.ColorId = variant.Color.Id;
            this.SizeId = variant.Size.Id;

            this.OriginalPrice = variant.Price;            
            this.SalePrice = variant.GetDiscountedPriceTax();
        }

        public bool IsOnSale() => SalePrice != OriginalPrice;
    }
}
