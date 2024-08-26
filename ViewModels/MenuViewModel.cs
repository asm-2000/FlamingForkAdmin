using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlamingForkAdmin.Models;
using FlamingForkAdmin.Repositories.Implementations;

namespace FlamingForkAdmin.ViewModels
{
    public partial class MenuViewModel: ObservableObject
    {
        #region Properties
        [ObservableProperty]
        private List<MenuItemModel>? _MenuItems;

        [ObservableProperty]
        private string? _IsFetching;

        [ObservableProperty]
        private string? _NoMenuItemsPresent;

        [ObservableProperty]
        private string _MenuOperationResponseMessage;

        [ObservableProperty]
        private string _MenuOperationResponseMessageVisibility;

        private MenuServiceRepository _MenuService;
        private INavigation _Navigation;
        #endregion

        public MenuViewModel(INavigation navigation)
        {
            MenuOperationResponseMessageVisibility = "False";
            _Navigation = navigation;
            _MenuService = new MenuServiceRepository();
            FetchMenuItems();
        }

        [RelayCommand]
        public async Task NavigateBack()
        {
            await _Navigation.PopAsync();
        }

        [RelayCommand]
        public async Task FetchMenuItems()
        {
            IsFetching = "True";
            MenuItems = await _MenuService.GetAllMenuItems();
            if(MenuItems.Count == 0)
            {
                NoMenuItemsPresent = "True";
            }
            else
            {
                NoMenuItemsPresent = "False";
            }
            IsFetching = "False";
        }

        [RelayCommand]
        public async Task AddMenuItem()
        {
            return;
        }

        [RelayCommand]
        public async Task DeleteMenuItem()
        {
            return;
        }
    }
}
