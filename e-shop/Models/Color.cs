using ConsoleApp1;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;


namespace e_shop.Models
{
    [Table("tb_colors")]
    public class Color
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("color")]
        public int ColorCode { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual List<Variant> Variants { get; set; }
    }
}
