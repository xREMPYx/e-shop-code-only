using ConsoleApp1;
using System.ComponentModel.DataAnnotations.Schema;



namespace e_shop.Models
{
    [Table("tb_sizes")]
    public class Size
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("size")]
        public int Size_Number { get; set; }
        public virtual List<Variant> Variants { get; set; }
    }
}
