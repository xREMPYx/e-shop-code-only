using System.ComponentModel.DataAnnotations.Schema;

namespace e_shop.Models
{
    [Table("tb_orders")]
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }        

        [Column("shipping_method")]        
        public int ShippingMethod { get; set; }

        [Column("order_date")]
        public DateTime Order_Date { get; set; }

        [Column("total_tax")]
        public int TotalTax { get; set; }

        [Column("total_no_tax")]
        public int TotalNoTax { get; set; }

        [ForeignKey("customer_detail_id")]
        public virtual CustomerDetail CustomerDetails { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}