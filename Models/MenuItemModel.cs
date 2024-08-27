using CommunityToolkit.Mvvm.ComponentModel;

namespace FlamingForkAdmin.Models
{
    public class MenuItemModel
    {
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public int ItemPrice { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemCategory { get; set; }
        public string? ItemImageUrl { get; set; }

        public MenuItemModel(string itemName, int itemPrice, string itemCategory, string itemImageUrl)
        {
            ItemName = itemName;
            ItemCategory = itemCategory;
            ItemPrice = itemPrice;
            ItemImageUrl = itemImageUrl;
        }
    }
}
