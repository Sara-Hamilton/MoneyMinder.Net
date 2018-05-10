using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMinder.Net.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Category() { }

        public Category(string name, int id = 0)
        {
            Name = name;
            CategoryId = id;
        }

        public override bool Equals(System.Object otherCategory)
        {
            if (!(otherCategory is Category))
            {
                return false;
            }
            else
            {
                Category newCategory = (Category)otherCategory;
                return this.CategoryId.Equals(newCategory.CategoryId);
            }
        }

        public override int GetHashCode()
        {
            return this.CategoryId.GetHashCode();
        }

    }
}
