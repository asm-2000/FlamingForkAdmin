using FlamingForkAdmin.ViewModels;

namespace FlamingForkAdmin.Pages;

public partial class BeingPreparedOrdersPage : ContentPage
{
	public BeingPreparedOrdersPage()
	{
		InitializeComponent();
		BindingContext = new BeingPreparedOrdersViewModel(Navigation);
	}
}
