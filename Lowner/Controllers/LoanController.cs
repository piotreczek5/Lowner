using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowner.Models;
using Lowner.Models.Contracts;
using Lowner.Repositories;
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

        public LoanController(ILoanRepository loanRepository, IUserRepository userRepository)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
        }


        [HttpPost]
        [Route("AddLoan")]
        public ActionResult<Loan> AddLoan(Loan item)
        {
            string errorString;

            if(LoanIsValid(item,out errorString))
            {
                return BadRequest(new JsonResult(errorString));
            }

            _loanRepository.AddLoan(item);

            return Ok(item);
        }

        private bool LoanIsValid(Loan item, out string errorString)
        {
            errorString = String.Empty;
            if(item.Quantity <= 0)
            {
                errorString += "Loan must be initiated with positive quantity.";
            }

            if (_loanRepository.GetLoans().Contains(item))
            {
                errorString += "Loan with those data already exists.";
            }

            if (_userRepository.GetUsers().Any(user => user.Name == item.Borrower) == false)
            {
                errorString += "Borrower does not exists." ;
            }

            if (_userRepository.GetUsers().Any(user => user.Name == item.Lender) == false)
            {
                errorString += "Lender does not exists.";
            }

            if (item.LoanPaymentDueDate < DateTime.Today)
            {
                errorString += "Date of loan cannot be in the past.";
            }

            if (item.Borrower == item.Lender )
            {
                errorString += "Lender cannot be the same as borrower.";
            }

            return errorString == String.Empty ? false : true;
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
            if(contract.LoanId < 0) 
            {
                return BadRequest(new JsonResult("Loan Id cannot be smaller than 0."));
            }

            if (contract.Quantity < 0)
            {
                return BadRequest(new JsonResult("Cannot pay negative quantity."));
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
        [Route("GetLoans")]
        public ActionResult<List<User>> GetLoans()
        {
            return Ok(_loanRepository.GetLoans());
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