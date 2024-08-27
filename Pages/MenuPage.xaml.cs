using FlamingForkAdmin.ViewModels;

namespace FlamingForkAdmin.Pages;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
		BindingContext = new MenuViewModel(Navigation);
	}
}
