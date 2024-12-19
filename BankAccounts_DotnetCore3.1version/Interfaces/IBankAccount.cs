using BankAccounts_DotnetCore3._1version.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccounts_DotnetCore3._1version.Interfaces
{
    public interface IBankAccount
    {
        Task<List<BankAccount>> GetBankAccounts();
        Task<BankAccount> GetBankAccountById(int bankAccountID);
        Task<BankAccount> AddBankAccount(BankAccount bankAccount);
        Task<BankAccount> DeleteBankAccountsById(int bankAccountID);
        Task<bool> UpdateBankAccounts(BankAccount bankAccount);
    }
}
