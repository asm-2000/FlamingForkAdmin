using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlamingForkAdmin.Models;
using FlamingForkAdmin.Repositories.Implementations;
using FlamingForkAdmin.Views;
using System.Collections.ObjectModel;

namespace FlamingForkAdmin.ViewModels
{
    public partial class MenuViewModel: ObservableObject
    {
        #region Properties
        [ObservableProperty]
        private List<MenuItemModel>? _MenuItems;

        [ObservableProperty]
        private ObservableCollection<MenuItemModel>? _FilteredMenuItems;

        [ObservableProperty]
        private string? _IsFetching;

        [ObservableProperty]
        private string? _NoMenuItemsPresent;

        [ObservableProperty]
        private string _MenuOperationResponseMessage;

        [ObservableProperty]
        private string _MenuOperationResponseMessageVisibility;

        [ObservableProperty]
        private string _SearchKey;

        private MenuServiceRepository _MenuService;
        private INavigation _Navigation;
        private CancellationTokenSource _cancellationTokenSource;
        #endregion

        public MenuViewModel(INavigation navigation)
        {
            FilteredMenuItems = new();  
            MenuOperationResponseMessageVisibility = "False";
            _Navigation = navigation;
            _MenuService = new MenuServiceRepository();
            FetchMenuItems();
        }
        [RelayCommand]
        #region Methods
        public async Task SearchMenuItems()
        {
            // Cancels the previous search operation if it's still running
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            try
            {
                // Debounce: wait for a short time before executing the search
                await Task.Delay(500, token);

                // Executes the search on a background thread to avoid blocking the UI thread
                await Task.Run(() =>
                {
                    if (token.IsCancellationRequested) return;

                    // Uses a local collection to avoid multiple UI updates
                    var filtered = new List<MenuItemModel>();

                    if (string.IsNullOrEmpty(SearchKey))
                    {
                        filtered.AddRange(MenuItems);
                    }
                    else if (!string.IsNullOrWhiteSpace(SearchKey))
                    {
                        foreach (var menuItem in MenuItems)
                        {
                            if (menuItem.ItemName.Contains(SearchKey, StringComparison.OrdinalIgnoreCase))
                            {
                                filtered.Add(menuItem);
                            }
                        }
                    }

                    // Switches back to the UI thread to update the collection
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        FilteredMenuItems = new ObservableCollection<MenuItemModel>(filtered);
                    });
                }, token);
            }
            catch (TaskCanceledException)
            {
                return;
            }
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
                FilteredMenuItems = new();
                foreach(MenuItemModel menuItem in MenuItems)
                {
                    FilteredMenuItems.Add(menuItem);
                }
                NoMenuItemsPresent = "False";
            }
            IsFetching = "False";
        }

        [RelayCommand]
        public async Task AddMenuItem()
        {
            await _Navigation.PushModalAsync(new AddMenuItemPopUpView());
        }

        [RelayCommand]
        public async Task DeleteMenuItem(string itemId)
        {
            IsFetching = "True";
            MenuOperationResponseMessage = await _MenuService.DeleteMenuItem(Convert.ToInt32(itemId));
            IsFetching = "False";
            MenuOperationResponseMessageVisibility= "True";
            await Task.Delay(1000);
            MenuOperationResponseMessageVisibility = "False";
            await FetchMenuItems();
        }

        [RelayCommand]
        public async Task EditMenuItemDetails(string itemId)
        {
            MenuItemModel? itemToUpdate = MenuItems.Find(item => Convert.ToString(item.ItemId) == itemId);
            await _Navigation.PushModalAsync(new AddMenuItemPopUpView(itemToUpdate));
        }
    }
    #endregion
}
