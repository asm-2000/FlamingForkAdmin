using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlamingForkAdmin.Models;
using FlamingForkAdmin.Repositories.Implementations;

namespace FlamingForkAdmin.ViewModels
{
    public partial class AddMenuItemViewModel : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        private int _ItemId;

        [ObservableProperty]
        private string? _ItemName;

        [ObservableProperty]
        private string? _ItemCategory;

        [ObservableProperty]
        private string? _ItemPrice;

        [ObservableProperty]
        private string? _ItemImageSource;

        [ObservableProperty]
        private string _ResponseMessage;

        [ObservableProperty]
        private string _ResponseMessageVisibility;

        private INavigation _Navigation;
        private MenuServiceRepository _MenuService;
        #endregion

        public AddMenuItemViewModel(INavigation navigation, MenuItemModel menuItem)
        {
            ResponseMessageVisibility = "False";
            _MenuService = new MenuServiceRepository();
            _Navigation = navigation;
            ItemId = menuItem.ItemId;
            ItemName = menuItem.ItemName;
            ItemCategory = menuItem.ItemCategory;
            ItemPrice = Convert.ToString(menuItem.ItemPrice);
            ItemImageSource = menuItem.ItemImageUrl;
        }

        public AddMenuItemViewModel(INavigation navigation)
        {
            ResponseMessageVisibility = "False";
            _MenuService = new MenuServiceRepository();
            _Navigation = navigation;
        }

        [RelayCommand]
        public async Task ClosePopup()
        {
            await _Navigation.PopModalAsync();
        }

        [RelayCommand]
        public async Task AddMenuItem()
        {
            MenuItemModel item = new(ItemName,Convert.ToInt32(ItemPrice), ItemCategory, ItemImageSource);
            ResponseMessage = await _MenuService.AddMenuItem(item);
            ResponseMessageVisibility = "True";
            await Task.Delay(1000);
            await _Navigation.PopModalAsync();
            return;
        }
    }
}
