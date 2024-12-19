using BankAccounts_DotnetCore3._1version.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccounts_DotnetCore3._1version.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Departement>> GetDepartMentDetails();
        Task<Departement> GetDepartmentDetailsById(int deptid);
        Task<int> AddDeparment(Departement deptdetail);
        Task<bool> DeleteDepartmentById(int deptid);
        Task<bool> UpdateDepartment(Departement deptdetail);
    }
}
