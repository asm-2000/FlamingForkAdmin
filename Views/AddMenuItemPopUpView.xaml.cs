using CommunityToolkit.Maui.Views;
using FlamingForkAdmin.Models;
using FlamingForkAdmin.ViewModels;

namespace FlamingForkAdmin.Views;

public partial class AddMenuItemPopUpView: ContentPage
{
	public AddMenuItemPopUpView()
	{
		InitializeComponent();
		BindingContext = new AddMenuItemViewModel(Navigation);

	}
	public AddMenuItemPopUpView(MenuItemModel item)
	{
		InitializeComponent();
		BindingContext = new AddMenuItemViewModel(Navigation, item);
	}
}
