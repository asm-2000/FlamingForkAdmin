using FlamingForkAdmin.ViewModels;

namespace FlamingForkAdmin.Pages;

public partial class AdminLoginPage : ContentPage
{
	public AdminLoginPage()
	{
		InitializeComponent();
		BindingContext = new AdminLoginViewModel(Navigation);
	}
}
