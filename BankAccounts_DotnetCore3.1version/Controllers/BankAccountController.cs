using BankAccounts_DotnetCore3._1version.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using BankAccounts_DotnetCore3._1version.Interfaces;

namespace BankAccounts_DotnetCore3._1version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccount _bankAccount;
        public BankAccountController(IBankAccount bankAccount)
        {
            _bankAccount = bankAccount;
        }
        [HttpGet]
        [Route("GetBankAccountDetails")]
        public async Task<IActionResult> GetBankAccountDetails()
        {
            try
            {
                var bankAccountData = await _bankAccount.GetBankAccounts();
                if (bankAccountData == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "No Data");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, bankAccountData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }

        }

        [HttpGet]
        [Route("{bankAccountId}")]
        public async Task<IActionResult> GetBankDetailsByBankId(int bankAccountId)
        {
            if (bankAccountId < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var bankAccountData = await _bankAccount.GetBankAccountById(bankAccountId);
                if (bankAccountData == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return StatusCode(StatusCodes.Status200OK, bankAccountData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server eror");
            }
        }

        [HttpPost]
        [Route("AddBankAccount")]
        public async Task<IActionResult> Post([FromBody] BankAccount bankAccountObj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var bankAccountData = await _bankAccount.AddBankAccount(bankAccountObj);
                    //Here we call the another route
                    return CreatedAtAction("GetBankDetailsByBankId", new { bankAccountId = bankAccountObj.BankAccountID }, bankAccountObj);
                    //return StatusCode(StatusCodes.Status201Created, bankData);
                }
            }
            catch (Exception ex)
            {//if you got any error we are using this statuscode:Status500InternalServerError
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> put(int id,[FromBody] BankAccount bankAccountObj)
        {
            try
            {
                if (id != bankAccountObj.BankAccountID)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {

                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                else
                {
                    var bankAccountData = await _bankAccount.UpdateBankAccounts(bankAccountObj);
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

        [HttpDelete]
        [Route("{bankAccountId}")]
        public async Task<IActionResult> delete(int bankAccountId)
        {
            if (bankAccountId < 0)
            {//If input parameters are wrongly sent or empty, we will get 400 badrequest statuscode:Status400BadRequest
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var bankData = await _bankAccount.DeleteBankAccountsById(bankAccountId);
                if (bankData == null)
                {//in db if you get empty data we need to retrun this statuscode:Status404NotFound
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, bankData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
    }
}
