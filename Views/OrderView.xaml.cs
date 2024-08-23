using CommunityToolkit.Mvvm.Input;

namespace FlamingForkAdmin.Views;

public partial class OrderView : ContentView
{
    public OrderView()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty OrderIdProperty =
        BindableProperty.Create(nameof(OrderId), typeof(string), typeof(OrderView), default(string));

    public string OrderId
    {
        get => (string)GetValue(OrderIdProperty);
        set => SetValue(OrderIdProperty, value);
    }

    public static readonly BindableProperty OrderStatusProperty =
        BindableProperty.Create(nameof(OrderStatus), typeof(string), typeof(OrderView), default(string));

    public string OrderStatus
    {
        get => (string)GetValue(OrderStatusProperty);
        set => SetValue(OrderStatusProperty, value);
    }

    public static readonly BindableProperty CustomerAddressProperty =
        BindableProperty.Create(nameof(CustomerAddress), typeof(string), typeof(OrderView), default(string));

    public string CustomerAddress
    {
        get => (string)GetValue(CustomerAddressProperty);
        set => SetValue(CustomerAddressProperty, value);
    }

    public static readonly BindableProperty OrderDateProperty =
        BindableProperty.Create(nameof(OrderDate), typeof(string), typeof(OrderView), default(string));

    public string OrderDate
    {
        get => (string)GetValue(OrderDateProperty);
        set => SetValue(OrderDateProperty, value);
    }

    public static readonly BindableProperty StringifiedItemsProperty =
        BindableProperty.Create(nameof(StringifiedItems), typeof(string), typeof(OrderView), default(string));

    public string StringifiedItems
    {
        get => (string)GetValue(StringifiedItemsProperty);
        set => SetValue(StringifiedItemsProperty, value);
    }

    public static readonly BindableProperty TotalPriceProperty =
        BindableProperty.Create(nameof(TotalPrice), typeof(string), typeof(OrderView), default(string));

    public string TotalPrice
    {
        get => (string)GetValue(TotalPriceProperty);
        set => SetValue(TotalPriceProperty, value);
    }

    public static readonly BindableProperty OrderStatusChangedCommandProperty =
        BindableProperty.Create(nameof(OrderStatusChangedCommand), typeof(IAsyncRelayCommand), typeof(OrderView));

    public IAsyncRelayCommand OrderStatusChangedCommand
    {
        get => (IAsyncRelayCommand)GetValue(OrderStatusChangedCommandProperty);
        set => SetValue(OrderStatusChangedCommandProperty, value);
    }
}
