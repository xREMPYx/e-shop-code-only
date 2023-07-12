using ConsoleApp1;

namespace e_shop.Models.ViewModels
{
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public int SalePrice { get; set; }
        public int OriginalPrice { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Full_Description { get; set; }
        public Category Category { get; set; }
        public List<Product> RelatedProducts { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Parameter> Parameters { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public int SelectedColorId { get; set; }
        public int SelectedSizeId { get; set; }
        public Feedback NewFeedback { get; set; } = new Feedback();
        public List<Feedback> Feedback { get; set; }

        private readonly MyContext _context;

        public ProductDetailViewModel()
        {

        }

        public ProductDetailViewModel(int? colorId, int? sizeId, Product product, List<Product> relatedProducts, MyContext context)
        {
            _context = context;

            Variant variant = GetVariant(colorId, sizeId, product);

            ProductId = variant.Product.Id;
            Category = variant.Product.Category;
            Title = variant.Product.Title;
            Photos = variant.Product.Photos;
            Parameters = variant.Product.Parameters;
            RelatedProducts = relatedProducts;

            SelectedColorId = variant.Color.Id;
            SelectedSizeId = variant.Size.Id;

            Description = variant.Product.Description;
            Full_Description = variant.Product.Full_Description;
            OriginalPrice = variant.Price;
            SalePrice = variant.GetDiscountedPriceTax();
            Feedback = variant.Product.Feedback;

            Colors = GetColors(variant);
            Sizes = GetSizes(variant);
        }

        private Variant GetVariant(int? colorId, int? sizeId, Product product)
        {
            List<Variant> variants = product.Variants.ToList();

            if (colorId == null || !variants.Any(v => v.Color.Id == colorId))
            {
                return variants.First();
            }
            else if (sizeId != null && variants.Any(v => v.Size.Id == sizeId))
            {
                return variants.Where(v => v.Color.Id == colorId && v.Size.Id == sizeId).First();
            }
            else
            {
                return variants.Where(v => v.Color.Id == colorId).First();
            }            
        }

        private List<Color> GetColors(Variant variant)
        {            
            return _context.Colors.Where(c => c.Variants.Any(v => v.Product == variant.Product)).ToList();
        }

        private List<Size> GetSizes(Variant variant)
        {
            return _context.Sizes.Where(s => s.Variants.Any(v => v.Product == variant.Product && v.Color == variant.Color)).ToList();
        }

        public bool IsOnSale() => SalePrice != OriginalPrice;
    }
}
