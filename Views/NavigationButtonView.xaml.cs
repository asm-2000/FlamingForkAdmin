using CommunityToolkit.Mvvm.Input;

namespace FlamingForkAdmin.Views;

public partial class NavigationButtonView : ContentView
{
    public NavigationButtonView()
    {
        InitializeComponent();
    }

    // Bindable Property for ImageSource
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(NavigationButtonView), default(ImageSource));

    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    // Bindable Property for OrderType
    public static readonly BindableProperty OrderTypeProperty =
        BindableProperty.Create(nameof(OrderType), typeof(string), typeof(NavigationButtonView), string.Empty);

    public string OrderType
    {
        get => (string)GetValue(OrderTypeProperty);
        set => SetValue(OrderTypeProperty, value);
    }

    // Command for handling button press
    public static readonly BindableProperty NavigateButtonPressedProperty =
        BindableProperty.Create(nameof(NavigateButtonPressed), typeof(IAsyncRelayCommand), typeof(NavigationButtonView));

    public IAsyncRelayCommand NavigateButtonPressed
    {
        get => (IAsyncRelayCommand)GetValue(NavigateButtonPressedProperty);
        set => SetValue(NavigateButtonPressedProperty, value);
    }
}
