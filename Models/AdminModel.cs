namespace FlamingForkAdmin.Models
{
    public class AdminModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }    
        public AdminModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
