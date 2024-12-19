using BankAccounts_DotnetCore3._1version.Models;
using Microsoft.EntityFrameworkCore;
namespace BankAccounts_DotnetCore3._1version.DbConnect
{
    public class BankAccountContext:DbContext
    {
        public BankAccountContext(DbContextOptions<BankAccountContext> options) : base(options)
        {

        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
    }
}
