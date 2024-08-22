using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlamingForkAdmin.Helper.Utilities;
using FlamingForkAdmin.Pages;

namespace FlamingForkAdmin.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        private string _DisplayModeButtonSource;

        private INavigation _Navigation;

        #endregion Properties

        #region Constructor

        public MainViewModel(INavigation navigation)
        {
            DisplayModeButtonSource = Application.Current.UserAppTheme == AppTheme.Light ? "dark_mode_icon.png" : "light_mode_icon.png";
            _Navigation = navigation;
            CheckLoginStatus();
        }

        #endregion Constructor

        #region Methods

        public async Task CheckLoginStatus()
        {
            string? token = await SecureStorageHandler.GetAuthenticationToken();
            if (token == "Not Found")
            {
                await _Navigation.PushModalAsync(new AdminLoginPage());
            }
        }

        [RelayCommand]
        public void ToggleDisplayMode()
        {
            Application.Current.UserAppTheme = Application.Current.UserAppTheme == AppTheme.Light ? AppTheme.Dark : AppTheme.Light;
            DisplayModeButtonSource = Application.Current.UserAppTheme == AppTheme.Light ? "dark_mode_icon.png" : "light_mode_icon.png";
        }

        [RelayCommand]
        public async Task LogOutAdmin()
        {
            // Clears all the saved data.
            SecureStorageHandler.ClearStorage();
            // Clears Shell's navigation stack.
            Shell.Current.Items.Clear();
            // Initialized the app to its initial state.
            App.Current.MainPage = new AppShell();
            // Opens Sign in page.
            await _Navigation.PushModalAsync(new AdminLoginPage());
        }

        #endregion Methods
    }
}
