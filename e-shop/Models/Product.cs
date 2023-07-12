using e_shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [Table("tb_products")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("full_description")]
        public string Full_Description { get; set; }

        [ForeignKey("category_id")]
        public virtual Category Category { get; set; }

        public virtual List<Parameter> Parameters { get; set; }
        
        public virtual List<Photo> Photos { get; set; }

        public virtual List<Feedback> Feedback { get; set; }

        public virtual List<Variant> Variants { get; set; }
    }
}
