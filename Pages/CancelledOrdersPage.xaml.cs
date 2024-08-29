using FlamingForkAdmin.ViewModels;

namespace FlamingForkAdmin.Pages;

public partial class CancelledOrdersPage : ContentPage
{
	public CancelledOrdersPage()
	{
		InitializeComponent();
		BindingContext = new CancelledOrdersViewModel(Navigation);
	}
}