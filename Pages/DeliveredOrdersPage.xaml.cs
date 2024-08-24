using FlamingForkAdmin.ViewModels;

namespace FlamingForkAdmin.Pages;

public partial class DeliveredOrdersPage : ContentPage
{
	public DeliveredOrdersPage()
	{
		InitializeComponent();
		BindingContext = new DeliveredOrdersViewModel();
	}
}
