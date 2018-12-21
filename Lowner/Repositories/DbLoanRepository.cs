using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowner.Context;
using Lowner.Models;

namespace Lowner.Repositories
{
    public class DbLoanRepository : ILoanRepository
    {
        private readonly LoanContext _dbContext;

        public DbLoanRepository(LoanContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddLoan(Loan loan)
        {
            _dbContext.Loans.Add(loan);
            _dbContext.SaveChanges();
        }

        public Loan GetLoanById(int id)
        {
            return _dbContext.Loans.Find(id);
        }

        public IEnumerable<Loan> GetLoans()
        {
            return _dbContext.Loans.AsEnumerable();
        }

        public void UpdateLoan(Loan loan)
        {
            _dbContext.Entry(loan).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
