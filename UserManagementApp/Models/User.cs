namespace UserManagementApp.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
        public bool PasswordRestricted { get; set; }
    }
} 