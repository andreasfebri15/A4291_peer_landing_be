using DAL.DTO.Req;
using DAL.DTO.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositoris.Service.Interfaces
{
    public interface ILoanService
    {

        Task<string> CreateLoan(ReqLoanDTO loan);

        Task<string> UpdateStatusLoan(ReqEditDtoLoan loan, string id);

        Task<List<ResLoanDto>> LoanList(string status);
    }
}
