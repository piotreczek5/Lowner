using Lowner.Context;
using Lowner.Models;
using Lowner.Models.Contracts;
using Lowner.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowner.Validators
{
    public class LoanValidator
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;

        public LoanValidator(ILoanRepository loanRepository, IUserRepository userRepository)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
        }

        public bool AddLoanIsValid(Loan item, out string errorString)
        {
            errorString = String.Empty;
            if (item.Quantity <= 0)
            {
                errorString += "Loan must be initiated with positive quantity.";
            }

            if (_loanRepository.GetLoans().Contains(item))
            {
                errorString += "Loan with those data already exists.";
            }

            if (_userRepository.GetUsers().Any(user => user.Name == item.Borrower) == false)
            {
                errorString += "Borrower does not exists.";
            }

            if (_userRepository.GetUsers().Any(user => user.Name == item.Lender) == false)
            {
                errorString += "Lender does not exists.";
            }

            if (item.LoanPaymentDueDate < DateTime.Today)
            {
                errorString += "Date of loan cannot be in the past.";
            }

            if (item.Borrower == item.Lender)
            {
                errorString += "Lender cannot be the same as borrower.";
            }

            return errorString == String.Empty ? false : true;
        }

        public bool PayLoanIsValid(PayLoanContract contract, out string errorString)
        {
            errorString = String.Empty;

            if (contract.LoanId < 0)
            {
                errorString += "Loan Id cannot be smaller than 0.";
            }

            if (contract.Quantity < 0)
            {
                errorString += "Cannot pay negative quantity.";
            }

            return errorString == String.Empty ? false : true;
        }
    }
}
