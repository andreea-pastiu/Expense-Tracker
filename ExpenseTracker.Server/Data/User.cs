using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Server.Data
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [InverseProperty("User")]
        public List<Expense> Expenses { get; set; }
        [InverseProperty("User")]
        public List<Income> Incomes { get; set; }
    }
}
