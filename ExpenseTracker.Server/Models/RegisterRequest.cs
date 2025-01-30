namespace ExpenseTracker.Server.Models
{
    public class RegisterRequest : AuthRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
