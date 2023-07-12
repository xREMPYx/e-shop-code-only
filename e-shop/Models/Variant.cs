using e_shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [Table("tb_variants")]
    public class Variant
    {
        [Column("id")]
        public int Id { get; set; }
        
        [ForeignKey("size_id")]
        public virtual Size Size { get; set; }
        
        [ForeignKey("color_id")]
        public virtual Color Color { get; set; }
        
        [ForeignKey("product_id")]
        public virtual Product Product { get; set; }

        [Column("tax_percent")]
        public int Tax { get; set; }

        [Column("price_with_tax")]
        public int Price { get; set; }

        [Column("discount_percent")]
        public int Discount { get; set; }

        [Column("discount_due_date")]
        public DateTime Discount_Due_Date { get; set; }

        [Column("stock_units")]
        public int Stock { get; set; }

        private int GetPrice()
        {
            if (Discount_Due_Date < DateTime.Now)
            {
                return Price;
            }

            return (int)(Price * (1 - Discount * 0.01));
        }
        public int GetDiscountedPriceTax() => GetPrice();
        public int GetDiscountedPriceNoTax() => (int)(GetPrice() * (1 - 0.01 * Tax));
    }
}
