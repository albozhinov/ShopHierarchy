namespace ShopHierarchy.Data
{
    public class ItemOrder
    {
        // Many to Many reletionship!!!
        public int ItemId { get; set; }

        public Item Item { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

    }
}
