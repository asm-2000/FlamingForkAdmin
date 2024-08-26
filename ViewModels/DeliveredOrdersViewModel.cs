using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlamingForkAdmin.Models;
using FlamingForkAdmin.Repositories.Implementations;

namespace FlamingForkAdmin.ViewModels
{
    public partial class DeliveredOrdersViewModel: ObservableObject
    {
        #region Properties
        [ObservableProperty]
        private List<OrderModel>? _DeliveredOrders;

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

        public DeliveredOrdersViewModel(INavigation navigation)
        {
            StatusChangeMessageVisibility = "False";
            _OrderService = new OrderServiceRepository();
            _Navigation = navigation;
            FetchDeliveredOrders();
        }

        [RelayCommand]
        public async Task NavigateBack()
        {
            await _Navigation.PopAsync();
        }

        [RelayCommand]
        public async Task FetchDeliveredOrders()
        {
            IsFetching = "True";
            DeliveredOrders = await _OrderService.GetDeliveredOrders();
            NoOrdersPresent = DeliveredOrders.Count == 0 ? "True" : "False";
            IsFetching = "False";
        }

        [RelayCommand]
        public async Task ChangeOrderStatus(Tuple<string, string> orderParameters)
        {
            var specificOrder = DeliveredOrders.Find(order => Convert.ToString(order.OrderId) == orderParameters.Item1);
            bool changeConfirmation = await App.Current.MainPage.DisplayAlert("Status Change Confirmation", $"Change order status from '{specificOrder.OrderStatus}' to '{orderParameters.Item2}'", "Yes", "No");
            if (changeConfirmation)
            {
                specificOrder.OrderStatus = orderParameters.Item2;
                StatusChangeResponseMessage = await _OrderService.UpdateOrderStatus(specificOrder);
                await FetchDeliveredOrders();
                StatusChangeMessageVisibility = "True";
                await Task.Delay(1000);
                StatusChangeMessageVisibility = "False";
            }
        }
    }
}
