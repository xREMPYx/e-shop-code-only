using ConsoleApp1;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_shop.Models
{
    [Table("tb_feedback")]
    public class Feedback
    {
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("product_id")]
        public virtual Product Product { get; set; }

        [Column("stars")]
        public int Stars { get; set; }

        [Column("message")]
        public string Message { get; set; }
    }
}
