using DAL.DTO.Req;
using DAL.DTO.Rest;
using DAL.Repositoris.Service;
using DAL.Repositoris.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BE_PEER.Controllers
{
    [Route("rest/v1/Loan/[action]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanservices;

        public LoanController(ILoanService loanservices)
        {
            _loanservices = loanservices;
        }

        [HttpPost]
        public async Task<IActionResult> Newloan(ReqLoanDTO loan)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Any())
                        .Select(x => new
                        {
                            Field = x.Key,
                            Messages = x.Value.Errors.Select(e => e.ErrorMessage).ToList()
                        }).ToList();
                    var errorMessage = new StringBuilder("Validation errors occured!");
                    return BadRequest(new RestBaseDTO<object>
                    {
                        Success = false,
                        Message = errorMessage.ToString(),
                        Data = errors
                    });
                }

                var res = await _loanservices.CreateLoan(loan);

                return Ok(new RestBaseDTO<string>
                {
                    Success = true,
                    Message = res.ToString(),
                    Data = res
                });
            }
            catch (Exception ex)
            {
                if (ex.Message == "Email already used")
                {
                    return BadRequest(new RestBaseDTO<object>
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    });
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new RestBaseDTO<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatusLoan([FromQuery] string id, ReqEditDtoLoan loan)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Any())
                        .Select(x => new
                        {
                            Field = x.Key,
                            Messages = x.Value.Errors.Select(e => e.ErrorMessage).ToList()
                        }).ToList();

                    var errorMessage = new StringBuilder("Validation error occured!");

                    return BadRequest(new RestBaseDTO<object>
                    {
                        Success = false,
                        Message = errorMessage.ToString(),
                        Data = errors
                    });
                }

                var res = await _loanservices.UpdateStatusLoan(loan, id);

                return Ok(new RestBaseDTO<string>
                {
                    Success = true,
                    Message = "Success update loan status",
                    Data = res
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RestBaseDTO<object>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> LoanList([FromQuery] string status)
        {
            try
            {
                var res = await _loanservices.LoanList(status);

                return Ok(new RestBaseDTO<object>
                {
                    Success = true,
                    Message = "Success get all loan data",
                    Data = res
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RestBaseDTO<object>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }
    }
}
