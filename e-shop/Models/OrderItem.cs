using ConsoleApp1;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_shop.Models
{
    [Table("tb_order_items")]
    public class OrderItem
    {
        [Column("id")]
        public int Id { get; set; }
        
        [ForeignKey("order_id")]
        public virtual Order Order { get; set; }

        [ForeignKey("variant_id")]
        public virtual Variant Variant{ get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("total_tax")]
        public int TotalTax { get; set; }

        [Column("total_no_tax")]
        public int TotalNoTax { get; set; }
    }
}
