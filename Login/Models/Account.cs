namespace Login.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}