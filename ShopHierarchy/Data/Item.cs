using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopHierarchy.Data
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public List<ItemOrder> Orders { get; set; } = new List<ItemOrder>();

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
