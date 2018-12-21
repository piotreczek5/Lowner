using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowner.Models;
using Lowner.Models.Contracts;
using Lowner.Repositories;
using Lowner.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lowner.Controllers
{
    [Route("api/loan")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;
        private readonly LoanValidator _loanValidator;

        public LoanController(ILoanRepository loanRepository, IUserRepository userRepository, LoanValidator loanValidator)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _loanValidator = loanValidator;
        }

        [HttpPost]
        [Route("AddLoan")]
        public ActionResult<Loan> AddLoan(Loan item)
        {
            string errorString;

            if(_loanValidator.AddLoanIsValid(item,out errorString))
            {
                return BadRequest(new JsonResult(errorString));
            }

            _loanRepository.AddLoan(item);

            return Ok(item);
        }

        [HttpGet]
        [Route("GetLoansForUser")]
        public ActionResult<List<Loan>> GetLoansForUser(string name)
        {
            var loanItems = _loanRepository.GetLoans().Where(loan => loan.Borrower == name || loan.Lender == name);

            if(loanItems == null)
            {
                return NotFound( new JsonResult($"No loans found for user: {name}"));
            }

            return Ok(loanItems.ToList());            
        }

        [HttpPost]
        [Route("PayLoan")]
        public ActionResult<Loan> PayLoan(PayLoanContract contract)
        {
            string errorString;
            if (_loanValidator.PayLoanIsValid(contract, out errorString))
            {
                return BadRequest(new JsonResult(errorString));
            }

            var loanItem = _loanRepository.GetLoanById(contract.LoanId);
            if (loanItem == null)
            {
                return NotFound(new JsonResult("Loan is not found."));
            }

            if (loanItem.Quantity - contract.Quantity < 0)
            {
                return BadRequest(new JsonResult("Quantity you want to pay is bigger than amount left on the loan."));
            }

            loanItem.Quantity -= contract.Quantity;
            _loanRepository.UpdateLoan(loanItem);

            return Ok(loanItem);
        }

        [HttpGet]
        [Route("GetBorrowersAndLenders")]
        public ActionResult<BorrowersLendersContract> GetBorrowersAndLenders()
        {
            BorrowersLendersContract borrowersLendersContract = new BorrowersLendersContract();

            borrowersLendersContract.Borrowers.AddRange(
                _loanRepository.GetLoans()
                .Select(loan => loan.Borrower)
                .Distinct());

            borrowersLendersContract.Lenders.AddRange(
               _loanRepository.GetLoans()
               .Select(loan => loan.Lender)
               .Distinct());

            return Ok(borrowersLendersContract);
        }
    }
}