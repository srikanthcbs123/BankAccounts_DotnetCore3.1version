using BankAccounts_DotnetCore3._1version.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using BankAccounts_DotnetCore3._1version.Models;

namespace BankAccounts_DotnetCore3._1version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBank _bank;
        public BankController(IBank bank)
        {
                _bank = bank;
        }
        [HttpGet]
        [Route("GetBankDetails")]
        public async Task<IActionResult> GetBankDetails()
        {
            try
            {
                var bankData = await _bank.GetBanks();
                if (bankData == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
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

        [HttpGet]
        [Route("{bankId}")]
        public async Task<IActionResult> GetBankDetailsByBankId(int bankId)
        {
            if (bankId < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var bankData = await _bank.GetBanksById(bankId);
                if (bankData ==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return StatusCode(StatusCodes.Status200OK, bankData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server eror");
            }
        }

        [HttpPost]
        [Route("AddBank")]
        public async Task<IActionResult> Post([FromBody] Bank bankObj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var bankData = await _bank.AddBanks(bankObj);
                    //Here we call the another route
                    return CreatedAtAction("GetBankDetailsByBankId", new { bankId = bankObj.BankID }, bankObj);
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
        public async Task<IActionResult> put(int id, [FromBody] Bank bankObj)
        {
            try
            {
                if (id != bankObj.BankID)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {

                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                else
                {
                    var empdata = await _bank.UpdateBank(bankObj);
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

        [HttpDelete]
        [Route("{bankId}")]
        public async Task<IActionResult> delete(int bankId)
        {
            if (bankId < 0)
            {//If input parameters are wrongly sent or empty, we will get 400 badrequest statuscode:Status400BadRequest
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var bankData = await _bank.DeleteBanksById(bankId);
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
