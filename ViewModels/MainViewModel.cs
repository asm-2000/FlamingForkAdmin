using FlamingForkAdmin.Pages;

namespace FlamingForkAdmin.ViewModels
{
    public partial class MainViewModel
    {
        private INavigation _Navigation;
        public MainViewModel(INavigation navigation)
        {
            _Navigation = navigation;
            CheckLoginStatus();
        }

        public async Task CheckLoginStatus()
        {
            await _Navigation.PushModalAsync(new AdminLoginPage());
        }
    }
}
