using BankAccounts_DotnetCore3._1version.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Serilog;
using BankAccounts_DotnetCore3._1version.Models;
namespace BankAccounts_DotnetCore3._1version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        public readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository=employeeRepository; 
        }

        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                Log.Information($"EmployeeController.GetEmployee method started");
                var empData = _employeeRepository.GetEmployees();
                if (empData == null) {
                Log.Information($"EmployeeController.GetEmployee method Ended");
                    return StatusCode(StatusCodes.Status404NotFound,"No Data Found");
                }
                else
                {

                    return StatusCode(StatusCodes.Status200OK, empData);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "No Data Found");
            }
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post([FromBody] Employee empdto)
        {
            try
            {
                Log.Information($"EmployeeController.AddEmployee method started");
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var empdata = await _employeeRepository.AddEmployes(empdto);
                    Log.Information($"EmployeeController.AddEmployee method ended");
                    return StatusCode(StatusCodes.Status201Created, empdata);
                }
            }
            catch (Exception ex)
            {//if you got any error we are using this statuscode:Status500InternalServerError
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpDelete]
        [Route("DeleteEmployeeByEmpid/{empid}")]
        public async Task<IActionResult> delete(int empid)
        {
            if (empid < 0)
            {//If input parameters are wrongly sent or empty, we will get 400 badrequest statuscode:Status400BadRequest
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                Log.Information($"EmployeeController.delete method started");
                var empdata = await _employeeRepository.DeleteEmployesById(empid);
                if (empdata == null)
                {//in db if you get empty data we need to retrun this statuscode:Status404NotFound
                    Log.Information($"EmployeeController.AddEmployee method ended");
                    return StatusCode(StatusCodes.Status404NotFound, "empdata not  found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
      
        [HttpGet]
        [Route("GetEmployeeByEmpid/{empid}")]
        public async Task<IActionResult> Get(int empid)
        {
            if (empid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                Log.Information($"EmployeeController.GetEmployeeByEmpid method started");
                var empdata = await _employeeRepository.GetEmployeeById(empid);
                Log.Information($"EmployeeController.GetEmployeeByEmpid method ended");
                return StatusCode(StatusCodes.Status200OK, empdata);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server eror");
            }
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> put([FromBody] Employee empdto)
        {
            try
            {
                Log.Information($"EmployeeController.UpdateEmployee method started");
                if (!ModelState.IsValid)
                {

                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var empdata = await _employeeRepository.UpdateEmploye(empdto);
                    Log.Information($"EmployeeController.UpdateEmployee method ended");
                    return StatusCode(StatusCodes.Status200OK, empdata);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
    }
}
