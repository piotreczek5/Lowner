using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lowner.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        public string Lender { get; set; }
        public string Borrower { get; set; }
        public decimal Quantity { get; set; }
        public DateTime LoanPaymentDueDate{ get; set; }

        public override bool Equals(object obj)
        {
            Loan loan = (Loan)obj;

            if (loan == null)
            {
                return false;
            }

            return loan.Lender.Equals(Lender) &&
                loan.Borrower.Equals(Borrower) &&
                loan.LoanPaymentDueDate == LoanPaymentDueDate;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
