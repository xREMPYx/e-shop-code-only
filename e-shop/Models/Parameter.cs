using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [Table("tb_parameters")]
    public class Parameter
    {
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("product_id")]
        public virtual Product Product { get; set; }

        [Column("name")]
        public string Value { get; set; }

        [Column("title")]
        public string Title { get; set; }
    }
}
