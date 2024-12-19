using BankAccounts_DotnetCore3._1version.DbConnect;
using BankAccounts_DotnetCore3._1version.Interfaces;
using BankAccounts_DotnetCore3._1version.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccounts_DotnetCore3._1version.Respository
{
    public class BankAccountRepository : IBankAccount
    {
        private readonly BankAccountContext _context;
        public BankAccountRepository(BankAccountContext context)
        {
                _context = context;
        }
        public async Task<List<BankAccount>> GetBankAccounts()
        {
            var result = await _context.BankAccounts.ToListAsync();
            if (result.Count == 0)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
        public async Task<BankAccount> GetBankAccountById(int bankAccountID)
        {
            var rm = await _context.BankAccounts.Where(e => e.BankAccountID == bankAccountID).FirstOrDefaultAsync();

            if (rm == null)
                return null;
            else
                return rm;
        }
        public async Task<BankAccount> AddBankAccount(BankAccount bankAccount)
        {
            await _context.BankAccounts.AddAsync(bankAccount);//add the record by using addasync
            _context.SaveChanges();//it will commit/save the data perminently in table
            return bankAccount;
        }

        public async Task<bool> UpdateBankAccounts(BankAccount bankAccount)
        {
            _context.BankAccounts.Update(bankAccount);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<BankAccount> DeleteBankAccountsById(int bankAccountID)
        {
            BankAccount rm = _context.BankAccounts.SingleOrDefault(e => e.BankAccountID == bankAccountID);
            if (rm != null)
            {//Here Remove() method is used for removing the data from database.

                _context.BankAccounts.Remove(rm);
                _context.SaveChanges();
                return rm;
            }
            else return null;
        }
    }
}
