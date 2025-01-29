using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Server.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Income> Income { get; set; }
        public DbSet<Expense> Expense { get; set; }
    }
}
