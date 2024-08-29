using System.Text.RegularExpressions;

namespace FlamingForkAdmin.Helper.Utilities
{
    public static class ValidationService
    {
        #region EmailValidator

        // Summary:
        // Checks email for null values, matches the regex pattern and returns proper
        // error message.
        public static string EmailValidator(string email)
        {
            string emailError;
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new(emailPattern);
            if (string.IsNullOrWhiteSpace(email))
            {
                emailError = "Email field cannot be empty!";
            }
            else if (!regex.IsMatch(email))
            {
                emailError = "Please enter a valid Email address";
            }
            else
            {
                emailError = string.Empty;
            }

            return emailError;
        }

        #endregion EmailValidator

        #region PasswordValidator
        // Summary:
        // Checks password for null values, matches the regex pattern and returns proper
        // error message.
        public static string PasswordValidator(string password)
        {
            string passwordError;
            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            Regex regex = new(passwordPattern);
            if (string.IsNullOrWhiteSpace(password))
            {
                passwordError = "Password field cannot be empty!";
            }
            else if (!regex.IsMatch(password))
            {
                passwordError = "Please enter a valid password";
            }
            else
            {
                passwordError = string.Empty;
            }

            return passwordError;
        }

        #endregion PasswordValidator
    }
}
