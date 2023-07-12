using System.ComponentModel.DataAnnotations.Schema;

namespace e_shop.Models
{
    [Table("tb_customer_details")]
    public class CustomerDetail
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("contact")]
        public string Contact { get; set; }

        [Column("first_name")]
        public string First_Name { get; set; }

        [Column("last_name")]
        public string Last_Name { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("street")]
        public string Street { get; set; }

        [Column("apartment")]
        public string Apartment { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [Column("postal_code")]
        public string Postal_Code { get; set; }
        
        public virtual Order Order { get; set; }
    }
}
