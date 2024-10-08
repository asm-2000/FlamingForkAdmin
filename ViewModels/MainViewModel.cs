﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlamingForkAdmin.Helper.Utilities;
using FlamingForkAdmin.Models;
using FlamingForkAdmin.Pages;
using FlamingForkAdmin.Repositories.Implementations;

namespace FlamingForkAdmin.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        private List<OrderModel> _AllOrders;

        [ObservableProperty]
        private string _DisplayModeButtonSource;

        [ObservableProperty]
        private string _IsFetching;

        [ObservableProperty]
        private string _NoOrdersPresent;

        [ObservableProperty]
        private string _StatusChangeResponseMessage;

        [ObservableProperty]
        private string _StatusChangeMessageVisibility;

        private INavigation _Navigation;
        private OrderServiceRepository _OrderService;

        #endregion Properties

        #region Constructor

        public MainViewModel(INavigation navigation)
        {
            DisplayModeButtonSource = Application.Current.UserAppTheme == AppTheme.Light ? "dark_mode_icon.png" : "light_mode_icon.png";
            _Navigation = navigation;
            AllOrders = [];
            StatusChangeMessageVisibility = "False";
            _OrderService = new OrderServiceRepository();
            CheckLoginStatus();
            FetchAllOrders();
        }

        #endregion Constructor

        #region Methods

        public async Task CheckLoginStatus()
        {
            string? token = await SecureStorageHandler.GetAuthenticationToken();
            if (token == "Not Found")
            {
                await _Navigation.PushModalAsync(new AdminLoginPage());
            }
        }

        [RelayCommand]
        public async Task FetchAllOrders()
        {
            IsFetching = "True";
            string? token = await SecureStorageHandler.GetAuthenticationToken();
            while(AllOrders.Count == 0)
            {
                if (token != "Not Found")
                {
                    AllOrders = await _OrderService.GetAllOrders();
                    AllOrders = AllOrders.OrderByDescending(order => order.OrderDate).ToList();
                    NoOrdersPresent = AllOrders.Count == 0 ? "True" : "False";
                }
            }
            IsFetching = "False";
        }

        [RelayCommand]
        public async Task RefreshAllOrders()
        {
            IsFetching = "True";
            AllOrders = await _OrderService.GetAllOrders();
            AllOrders = AllOrders.OrderByDescending(order => order.OrderDate).ToList();
            NoOrdersPresent = AllOrders.Count == 0 ? "True" : "False";
            IsFetching = "False";
        }

        [RelayCommand]
        public void ToggleDisplayMode()
        {
            Application.Current.UserAppTheme = Application.Current.UserAppTheme == AppTheme.Light ? AppTheme.Dark : AppTheme.Light;
            DisplayModeButtonSource = Application.Current.UserAppTheme == AppTheme.Light ? "dark_mode_icon.png" : "light_mode_icon.png";
        }

        [RelayCommand]
        public async Task ChangeOrderStatus(Tuple<string,string> orderParameters)
        {
            await RefreshAllOrders();
            var specificOrder = AllOrders.Find(order => Convert.ToString(order.OrderId) == orderParameters.Item1);
            bool changeConfirmation = await App.Current.MainPage.DisplayAlert("Status Change Confirmation",$"Change order status from '{specificOrder.OrderStatus}' to '{orderParameters.Item2}'","Yes","No");
            if(changeConfirmation)
            {
                specificOrder.OrderStatus = orderParameters.Item2;
                StatusChangeResponseMessage = await _OrderService.UpdateOrderStatus(specificOrder);
                await RefreshAllOrders();
                StatusChangeMessageVisibility = "True";
                await Task.Delay(1000);
                StatusChangeMessageVisibility = "False";
            }
        }

        [RelayCommand]
        public async Task NavigateToReceivedOrdersPage()
        {
            await _Navigation.PushAsync(new ReceivedOrdersPage());
        }

        [RelayCommand]
        public async Task NavigateToBeingPreparedOrdersPage()
        {
            await _Navigation.PushAsync(new BeingPreparedOrdersPage());
        }

        [RelayCommand]
        public async Task NavigateToBeingDeliveredOrdersPage()
        {
            await _Navigation.PushAsync(new BeingDeliveredOrdersPage());
        }

        [RelayCommand]
        public async Task NavigateToDeliveredOrdersPage()
        {
            await _Navigation.PushAsync(new DeliveredOrdersPage());
        }

        [RelayCommand]
        public async Task NavigateToCancelledOrdersPage()
        {
            await _Navigation.PushAsync(new CancelledOrdersPage());
        }

        [RelayCommand]
        public async Task NavigateToMenuPage()
        {
            await _Navigation.PushAsync(new MenuPage());
        }

        [RelayCommand]
        public async Task LogOutAdmin()
        {
            // Clears all the saved data.
            SecureStorageHandler.ClearStorage();
            // Clears Shell's navigation stack.
            Shell.Current.Items.Clear();
            // Initialized the app to its initial state.
            App.Current.MainPage = new AppShell();
            // Opens Sign in page.
            await _Navigation.PushModalAsync(new AdminLoginPage());
        }

        #endregion Methods
    }
}