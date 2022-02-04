using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopHierarchy.Data
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int SalesmanId { get; set; }

        public SalesMan Salesman { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
