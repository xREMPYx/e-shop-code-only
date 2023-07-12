using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [Table("tb_images")]
    public class Photo
    {
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("product_id")]
        public virtual Product Product { get; set; }

        [Column("priority")]
        public int Priority { get; set; }

        [Column("image_name")]
        public string Name { get; set; }
    }
}
