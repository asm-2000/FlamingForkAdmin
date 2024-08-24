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

        private OrderServiceRepository _OrderService;
        private INavigation _Navigation;
        #endregion

        public ReceivedOrdersViewModel(INavigation navigation)
        {
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
    }
}
