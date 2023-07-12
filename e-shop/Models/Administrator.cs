using System.ComponentModel.DataAnnotations.Schema;

namespace e_shop.Models
{
    [Table("tb_administrators")]
    public class Administrator
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; }
        
        [Column("password")]
        public string Password { get; set; }
    }
}
