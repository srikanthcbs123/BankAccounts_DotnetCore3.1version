using BankAccounts_DotnetCore3._1version.Interfaces;
using BankAccounts_DotnetCore3._1version.Models;
using BankAccounts_DotnetCore3._1version.utils;
using Dapper;
using Serilog;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccounts_DotnetCore3._1version.Respository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        IConnectionFactory _connectionFactory;
        //Constructor injection for iplemeting loosly coupled between the classes.
        public EmployeeRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            Log.Information($"EmployeeRepository.GetEmployees method started");
            List<Employee> res;
            using (IDbConnection conn = _connectionFactory.hotelmanagementsqlConnectionString())
            {
                var queryresult = await conn.QueryAsync<Employee>(StoredProcedureNames.GetEmployee, CommandType.StoredProcedure);
                res = queryresult.ToList();
                Log.Information($"EmployeeRepository.GetEmployees method ended");
                return res;
            }
        }
        public async Task<Employee> GetEmployeeById(int empid)
        {
            Log.Information($"EmployeeRepository.GetEmployeeById method started");
            Employee emp;
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add("@empid", empid);
                var result = await con.QueryAsync<Employee>(StoredProcedureNames.GetEmployeeByEmpid, p, commandType: CommandType.StoredProcedure);
                emp = result.FirstOrDefault();
                Log.Information($"EmployeeRepository.GetEmployeeById method ended");
                return emp;
            }
        }
        public async Task<int> AddEmployes(Employee empdetail)
        {
            Log.Information($"EmployeeRepository.AddEmployes method started");
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlConnectionString())
            {//Create object for DynamicParameters for storedure input parameter values binding purpose used.
                var p = new DynamicParameters();//DynamicParameters comming from Dapper package
                p.Add("@empname", empdetail.empname);
                p.Add("@empsalary", empdetail.empsalary);
                p.Add("@insertvalue", DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteScalarAsync<int>(StoredProcedureNames.AddEmployee, p, commandType: CommandType.StoredProcedure);
                int inserterdid = p.Get<int>("@insertvalue");
                Log.Information($"EmployeeRepository.AddEmployes method ended");
                return inserterdid;
            }
        }

        public async Task<bool> UpdateEmploye(Employee empdetail)
        {
            Log.Information($"EmployeeRepository.UpdateEmploye method started");
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add("@empid", empdetail.empid);
                p.Add("@empname", empdetail.empname);
                p.Add("@empsalary", empdetail.empsalary);
                await con.ExecuteReaderAsync(StoredProcedureNames.UpdateEmployee, p, commandType: CommandType.StoredProcedure);
                Log.Information($"EmployeeRepository.UpdateEmploye method ended");
                return true;
            }
        }
        public async Task<bool> DeleteEmployesById(int empid)
        {
            Log.Information($"EmployeeRepository.DeleteEmployesById method started");
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlConnectionString())
            {
                var p = new DynamicParameters();
                p.Add("@empid", empid);
                await con.ExecuteScalarAsync(StoredProcedureNames.DeleteEmployee, p, commandType: CommandType.StoredProcedure);
                Log.Information($"EmployeeRepository.DeleteEmployesById method ended");
                return true;
            }
        }

    }
}
