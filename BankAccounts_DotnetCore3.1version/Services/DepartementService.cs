using BankAccounts_DotnetCore3._1version.Interfaces;
using BankAccounts_DotnetCore3._1version.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccounts_DotnetCore3._1version.Services
{
    public class DepartementService : IDepartementService
    {
        private readonly IDepartmentRepository _repository;
        public DepartementService(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> AddDeparment(DepartementDto deptdetail)
        {
            Departement objDept = new Departement();
            objDept.deptname = deptdetail.deptname;
            objDept.deptlocation = deptdetail.deptlocation;
            objDept.deptid = deptdetail.deptid;
            var res = await _repository.AddDeparment(objDept);
            return res;
        }

        public async Task<bool> DeleteDepartmentById(int deptid)
        {
            await _repository.DeleteDepartmentById(deptid);
            return true;
        }


        public async Task<List<DepartementDto>> GetDepartMentDetails()
        {

            List<DepartementDto> lstempdto = new List<DepartementDto>();
            var res = await _repository.GetDepartMentDetails();
            foreach (Departement dept in res)
            {
                DepartementDto deptdto = new DepartementDto();
                deptdto.deptid = dept.deptid;
                deptdto.deptname = dept.deptname;
                deptdto.deptlocation = dept.deptlocation;
                lstempdto.Add(deptdto);

            }
            return lstempdto;
        }

        public async Task<DepartementDto> GetDepartmentDetailsById(int deptid)
        {
            var res = await _repository.GetDepartmentDetailsById(deptid);
            DepartementDto deptdto = new DepartementDto();
            deptdto.deptid = res.deptid;
            deptdto.deptname = res.deptname;
            deptdto.deptlocation = res.deptlocation;
            return deptdto;
        }

        public async Task<bool> UpdateDepartment(DepartementDto deptdetail)
        {
            Departement objDept = new Departement();
            objDept.deptid = deptdetail.deptid;
            objDept.deptname = deptdetail.deptname;
            objDept.deptlocation = deptdetail.deptlocation;
            await _repository.UpdateDepartment(objDept);
            return true;
        }
    }
}
