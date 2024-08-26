using FlamingForkAdmin.ViewModels;

namespace FlamingForkAdmin.Pages;

public partial class BeingDeliveredOrdersPage : ContentPage
{
	public BeingDeliveredOrdersPage()
	{
		InitializeComponent();
		BindingContext = new BeingDeliveredOrdersViewModel(Navigation);
	}
}
