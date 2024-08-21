using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FlamingForkAdmin.ViewModels
{
    public partial class AdminLoginViewModel:ObservableObject
    {
        #region Properties

        [ObservableProperty]
        private string _Email;

        [ObservableProperty]
        private string _Password;

        [ObservableProperty]
        private string _EmailError;

        [ObservableProperty]
        private string _PasswordError;

        [ObservableProperty]
        private string _SignInMessage;

        [ObservableProperty]
        private bool _IsSigningIn;

        private bool _FormValidity;

        private INavigation _Navigation;

        private AuthenticationServiceRepository _AuthService;

        #endregion Properties

        public AdminLoginViewModel(INavigation navigation)
        {
            _Navigation = navigation;
            _AuthService = new AuthenticationServiceRepository();
            _FormValidity = false;
            _IsSigningIn = false;
            _EmailError = "";
            _PasswordError = "";
            _SignInMessage = "";
        }

        #region Validation Methods

        [RelayCommand]
        public void ValidateEmail()
        {
            EmailError = Validation.EmailValidator(Email);
        }

        [RelayCommand]
        public void ValidatePassword()
        {
            PasswordError = Validation.PasswordValidator(Password);
        }

        [RelayCommand]
        public async Task ValidateForm()
        {
            _FormValidity = string.IsNullOrEmpty(EmailError = Validation.EmailValidator(Email)) && string.IsNullOrEmpty(PasswordError = Validation.PasswordValidator(Password));
            if (_FormValidity)
            {
                IsSigningIn = true;
                CustomerLoginModel customerCredentials = new CustomerLoginModel(Email, Password);
                SignInMessage = await _AuthService.LoginCustomer(customerCredentials);
                string token = await SecureStorageHandler.GetAuthenticationToken();
                IsSigningIn = false;
                if (token != "Not Found")
                {
                    await _Navigation.PopModalAsync();
                }
            }
        }

        #endregion Validation Methods
    }
}
