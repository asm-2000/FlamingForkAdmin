namespace FlamingForkAdmin.Models
{
    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string? OrderItemName { get; set; }
        public int OrderItemPrice { get; set; }
        public int Quantity { get; set; }
    }
}
