using FlamingForkAdmin.ViewModels;

namespace FlamingForkAdmin.Pages;

public partial class ReceivedOrdersPage : ContentPage
{
	public ReceivedOrdersPage()
	{
		InitializeComponent();
		BindingContext = new ReceivedOrdersViewModel(Navigation);
	}
}
