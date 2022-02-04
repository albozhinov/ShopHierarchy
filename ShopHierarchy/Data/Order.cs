using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopHierarchy.Data
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<ItemOrder> Items { get; set; } = new List<ItemOrder>();
    }
}
