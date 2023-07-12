using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_shop.Models
{
    [Table("tb_categories")]
    public class Category
    {        
        [Column("id")]
        public int? Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [ForeignKey("parent_id")]
        public virtual Category Parent { get; set; }

        public virtual List<Category> SubCategories { get; set; }

        public List<Category> GetParents()
        {
            List<Category> result = new List<Category>();

            Category parent = Parent;

            while (parent != null)
            {
                result.Add(parent);

                parent = parent.Parent;
            }

            result.Reverse();

            return result;
        }

        public List<Category> GetAllChildren()
        {
            List<Category> result = new List<Category>();

            return GetAllChildren(this, result);
        }

        private List<Category> GetAllChildren(Category category, List<Category> result)
        {
            result.Add(category);

            foreach (Category subCategory in category.SubCategories)
            {                
                GetAllChildren(subCategory, result);
            }

            return result;
        }
    }
}
