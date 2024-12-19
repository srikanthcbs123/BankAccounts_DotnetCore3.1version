using BankAccounts_DotnetCore3._1version.DbConnect;
using BankAccounts_DotnetCore3._1version.Interfaces;
using BankAccounts_DotnetCore3._1version.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccounts_DotnetCore3._1version.Respository
{
    public class BankRepository : IBank
    {
        private readonly BankAccountContext _context;
        public BankRepository(BankAccountContext context)
        {
            _context=context;
        }
        public async Task<List<Bank>> GetBanks()
        {
            var result = await _context.Banks.ToListAsync();
            if (result.Count == 0)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async  Task<Bank> GetBanksById(int bankID)
        {
            var rm = await _context.Banks.Where(e => e.BankID == bankID).FirstOrDefaultAsync();

            if (rm == null)
                return null;
            else
                return rm;
        }
        public async Task<Bank> AddBanks(Bank bankdetail)
        {
            await _context.Banks.AddAsync(bankdetail);//add the record by using addasync method.it is predefined.
            _context.SaveChanges();//it will commit/save the data perminently in table
            return bankdetail;
        }

        public async Task<bool> UpdateBank(Bank bankdetail)
        {
            _context.Banks.Update(bankdetail);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<Bank> DeleteBanksById(int bankID)
        {
            Bank rm = _context.Banks.SingleOrDefault(e => e.BankID == bankID);
            if (rm != null)
            {//Here Remove() method is used for removing the data from database.

                _context.Banks.Remove(rm);
                _context.SaveChanges();
                return rm;
            }
            else return null;
        }
    }
}
