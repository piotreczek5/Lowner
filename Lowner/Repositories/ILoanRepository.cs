using Lowner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowner.Repositories
{
    public interface ILoanRepository
    {
        void AddLoan(Loan loan);
        IEnumerable<Loan> GetLoans();
        Loan GetLoanById(int id);
        void UpdateLoan(Loan loan);
    }
}
