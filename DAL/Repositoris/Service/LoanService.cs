using DAL.DTO.Req;
using DAL.DTO.Rest;
using DAL.Models;
using DAL.Repositoris.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositoris.Service
{
    public class LoanService : ILoanService
    {

        private readonly CustomerContext _peerlandingcontext;

        public LoanService (CustomerContext peerlandingcontext)
        {
            _peerlandingcontext = peerlandingcontext;
        }

        public async Task<string> CreateLoan(ReqLoanDTO loan)
        {
            var newloan = new MstLoans
            {
                BorrowerId = loan.BorrowerId,
                Amount = loan.Amount,
                InteresestRate = loan.InterestRate,
                Duration = loan.Duration,
            };
            await _peerlandingcontext.AddAsync(newloan);
            await _peerlandingcontext.SaveChangesAsync();

            return newloan.BorrowerId;
        }



        public async Task<List<ResLoanDto>> LoanList(string status)
        {

            var loans = await _peerlandingcontext.MstLoans.
                Include(l => l.User).
                Select(x => new ResLoanDto
                {
                    LoanId = x.Id,
                    BorrowerName = x.User.Name,
                    Amount = x.Amount,
                    InterestRate = x.InteresestRate,
                    Duration = x.Duration,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).OrderBy(x => x.CreatedAt)
            .Where(x => string.IsNullOrEmpty(status) || x.Status == status)
            .ToListAsync();


            return loans;


        }

        public async Task<string>  UpdateStatusLoan(ReqEditDtoLoan loan, string id)
        {
            var borrower = _peerlandingcontext.MstLoans.FirstOrDefault(x => x.Id == id);

            if (borrower == null)
            {
                throw new Exception("Loan id not found!");
            }

            borrower.Status = loan.Status;


            _peerlandingcontext.MstLoans.Update(borrower);
            _peerlandingcontext.SaveChanges();

            return id;

        }

    }
    }
