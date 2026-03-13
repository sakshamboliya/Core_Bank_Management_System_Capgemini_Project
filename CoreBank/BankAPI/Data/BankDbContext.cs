using Microsoft.EntityFrameworkCore;
using BankAPI.Models;

namespace BankAPI.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}