using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccounts_DotnetCore3._1version.Interfaces
{
    public interface IDepartementService
    {
        Task<List<DepartementDto>> GetDepartMentDetails();
        Task<DepartementDto> GetDepartmentDetailsById(int deptid);
        Task<int> AddDeparment(DepartementDto deptdetail);
        Task<bool> DeleteDepartmentById(int deptid);
        Task<bool> UpdateDepartment(DepartementDto deptdetail);
    }
}
