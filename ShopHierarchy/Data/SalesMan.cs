using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopHierarchy.Data
{
    public class SalesMan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int CustomerId { get; set; }

        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
