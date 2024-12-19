using BankAccounts_DotnetCore3._1version.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccounts_DotnetCore3._1version.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int empid);
        Task<int> AddEmployes(Employee empdetail);
        Task<bool> DeleteEmployesById(int empid);
        Task<bool> UpdateEmploye(Employee empdetail);
    }
}
