using FlamingForkAdmin.Models;

namespace FlamingForkAdmin.Repositories.Interfaces
{
    public interface IMenuServiceRepository
    {
        Task<string> AddMenuItem(MenuItemModel menuItem);
        Task<List<MenuItemModel>> GetAllMenuItems();
        Task<string> DeleteMenuItem(int itemId);
        Task<string> UpdateMenuItemDetails(MenuItemModel menuItem);
    }
}
