using BankAccounts_DotnetCore3._1version.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccounts_DotnetCore3._1version.Interfaces
{
    public interface IBank
    {
        Task<List<Bank>> GetBanks();
        Task<Bank> GetBanksById(int bankID);
        Task<Bank> AddBanks(Bank bankdetail);
        Task<Bank> DeleteBanksById(int bankID);
        Task<bool> UpdateBank(Bank bankdetail);
    }
}
