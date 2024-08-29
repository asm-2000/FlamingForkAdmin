using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlamingForkAdmin.Models;
using FlamingForkAdmin.Repositories.Implementations;

namespace FlamingForkAdmin.ViewModels
{
    public partial class ReceivedOrdersViewModel:ObservableObject
    {
        #region Properties
        [ObservableProperty]
        private List<OrderModel>? _ReceivedOrders;

        [ObservableProperty]
        private string? _IsFetching;

        [ObservableProperty]
        private string? _NoOrdersPresent;

        [ObservableProperty]
        private string _StatusChangeResponseMessage;

        [ObservableProperty]
        private string _StatusChangeMessageVisibility;

        private OrderServiceRepository _OrderService;
        private INavigation _Navigation;
        #endregion

        public ReceivedOrdersViewModel(INavigation navigation)
        {
            StatusChangeMessageVisibility = "False";
            _OrderService = new OrderServiceRepository();
            _Navigation = navigation;
            FetchReceivedOrders();
        }

        [RelayCommand]
        public async Task NavigateBack()
        {
            await _Navigation.PopAsync();
        }

        [RelayCommand]
        public async Task FetchReceivedOrders()
        {
            IsFetching = "True";
            ReceivedOrders = await _OrderService.GetPlacedOrders();
            NoOrdersPresent = ReceivedOrders.Count == 0 ? "True" : "False";
            IsFetching = "False";
        }

        [RelayCommand]
        public async Task ChangeOrderStatus(Tuple<string, string> orderParameters)
        {
            var specificOrder = ReceivedOrders.Find(order => Convert.ToString(order.OrderId) == orderParameters.Item1);
            bool changeConfirmation = await App.Current.MainPage.DisplayAlert("Status Change Confirmation", $"Change order status from '{specificOrder.OrderStatus}' to '{orderParameters.Item2}'", "Yes", "No");
            if (changeConfirmation)
            {
                specificOrder.OrderStatus = orderParameters.Item2;
                StatusChangeResponseMessage = await _OrderService.UpdateOrderStatus(specificOrder);
                await FetchReceivedOrders();
                StatusChangeMessageVisibility = "True";
                await Task.Delay(1000);
                StatusChangeMessageVisibility = "False";
            }
        }
    }
}
